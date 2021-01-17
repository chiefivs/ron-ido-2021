"use strict";

const { options } = require('less');

{
    // Требуется для формирования полного output пути
    let path = require('path');

    // Путь к выходной папке
    const bundleFolder = "wwwroot";

    const HTMLWebpackPlugin = require('html-webpack-plugin');
    const {CleanWebpackPlugin} = require('clean-webpack-plugin');
    const CopyPlugin = require('copy-webpack-plugin');
    const MiniCssExtractPlugin = require('mini-css-extract-plugin');
    const TerserWebpackPlugin = require('terser-webpack-plugin');
    const OptimizeCssAssetsPlugin = require('optimize-css-assets-webpack-plugin');
    const webpack = require('webpack');

    const isDev = process.env.NODE_ENV === 'development';
    const isProd = !isDev;

    const filename = ext => isDev ? `[name].${ext}` : `[name].[hash].${ext}`;

    const cssLoaders = extra => {
        const loaders = [
            {
                loader: MiniCssExtractPlugin.loader,
                options: {
                    hmr: isDev,
                    reloadAll: true
                },
            },
            'css-loader'
        ];

        if(extra) {
            loaders.push(extra);
        }

        return loaders;
    };

    const jsLoaders = () => {
        const loaders = [{
            loader: 'babel-loader',
            options: babelOptions()
        }];

        if(isDev) {
            loaders.push('eslint-loader');
        }

        return loaders;
    };

    const babelOptions = preset => {
        const options = {
            presets: [
                '@babel/preset-env'
            ],
            plugins: [
                '@babel/plugin-proposal-class-properties'
            ]
        };

        if(preset) {
            options.presets.push(preset);
        }

        return options;
    };

    const optimization = () => {
        const config = {
            splitChunks: {
                chunks: 'all'
            }
        };

        if(isProd){
            config.minimizer = [
                new OptimizeCssAssetsPlugin(),
                new TerserWebpackPlugin()
            ]
        }
        
        return config;
    };

    module.exports = {
        context: path.resolve(__dirname, 'ClientApp'),
        mode: 'development',
        // Точка входа в приложение
        entry: {
            index: ['@babel/polyfill', './index.js']
        },
        // Выходной файл
        output: {
            filename: '[name].js',
            path: path.resolve(__dirname, bundleFolder)
        },
        resolve:{
            extensions:['.js', '.ts'],
            alias: {
                '@modules': path.resolve(__dirname, 'ClientApp/modules'),
                '@templates': path.resolve(__dirname, 'ClientApp/templates'),
                '@samples': path.resolve(__dirname, 'ClientApp/templates/samples')
            }
        },
        optimization: optimization(),
        devServer: {
            port:4000,
            hot: isDev,
            historyApiFallback: true,
            proxy: {
                "/api": {
                    target: "http://localhost:5000",
                }
            }
        },
        devtool: isDev ? 'inline-source-map' : '',
        plugins: [
            new HTMLWebpackPlugin({
                template: './index.html',
                minify: {
                    collapseWhitespace: isProd
                }
            }),
            new CleanWebpackPlugin(),
            new CopyPlugin({
                patterns: [
                    {
                        from: path.resolve(__dirname, 'ClientApp/favicon.ico'),
                        to: path.resolve(__dirname, 'wwwroot')
                    }
                ]
            }),
            new MiniCssExtractPlugin({
                filename: filename('css')
            }),
            new webpack.ProvidePlugin({
                $: "jquery",
                jquery: "jquery",
                "window.jQuery": "jquery",
                jQuery: "jquery"
            })
        ],
        module: {
            rules: [
                {
                    test: /\.css$/,
                    use: cssLoaders()
                },
                {
                    test: /\.less$/,
                    use: cssLoaders('less-loader')
                },
                {
                    test: /\.(png|gif|svg|jpg|jpeg)$/,
                    loader: 'file-loader',
                    options: {
                        name: 'content/images/[name].[ext]'
                    }
                },
                {
                    test: /\.(ttf|woff|woff2|eot)$/,
                    loader: 'file-loader',
                    options: {
                        name: 'content/fonts/[name].[ext]'
                    }
                },
                {
                    test: /\.html$/,
                    loader: 'raw-loader'
                },
                {
                    test: /\.js$/,
                    exclude: /node_modules/,
                    use: jsLoaders()
                },
                {
                    test: /\.tsx?$/,
                    loader: "ts-loader",
                    exclude: /node_modules/,
                }
                //{
                //    test: /\.ts$/,
                //    exclude: /node_modules/,
                //    loader: {
                //        loader: 'babel-loader',
                //        options: babelOptions('@babel/preset-typescript')
                //    }
                //}
            ]
        }
    };

    //if(isDev) {
    //    module.exports.plugins.push(new webpack.SourceMapDevToolPlugin({
    //        "filename": "[file].map[query]",
    //        //"append": null,
    //        //"module": true,
    //        //"columns": true,
    //        //"lineToLine": false,
    //        //"noSources": false,
    //        //"namespace": ""
    //    }));
    //};
}