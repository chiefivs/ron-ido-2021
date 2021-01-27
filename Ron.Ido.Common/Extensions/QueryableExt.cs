using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Ron.Ido.Common.Extensions
{
    public static class QueryableExt
    {
        /// <summary>
        /// Накладывает фильтр "содержит" на свойство типа string
        /// </summary>
        /// <param name="source"></param>
        /// <param name="propertyName">Имя свойства</param>
        /// <param name="value">Параметр, по которому проводится сравнение</param>
        /// <param name="aliases">Имена свойств, участвующих в поиске дополнительно к основному</param>
        /// <returns></returns>
        public static IQueryable<TEntityType> WhereContains<TEntityType>(this IQueryable<TEntityType> source, string propertyName, string value, IEnumerable<string> aliases = null) where TEntityType : class
        {
            if (string.IsNullOrEmpty(value))
                return source;

            //if (source.ElementType.GetPropertyType(propertyName).IsSubclassOf(typeof(EntityObject)))
            //    propertyName += ".Name";

            ParameterExpression parameterExpression = Expression.Parameter(source.ElementType, "entity");

            Expression containsExpression = null;
            //TODO:  разбиение на слова не работает в постгрессе из-за горбатой lowercase
            var variants = new[] { value }; //value.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(v => v.Trim());
            foreach (var variant in variants)
            {
                if (containsExpression == null)
                    // ReSharper disable once PossibleMultipleEnumeration
                    containsExpression = CreateVariantContainsExpression(propertyName, variant, aliases, parameterExpression);
                else
                {
                    // ReSharper disable once PossibleMultipleEnumeration
                    var cExp = CreateVariantContainsExpression(propertyName, variant, aliases, parameterExpression);

                    containsExpression = Expression.Or(containsExpression, cExp);
                }
            }


            // ReSharper disable once AssignNullToNotNullAttribute
            LambdaExpression conditionExpression = Expression.Lambda(containsExpression, new [] { parameterExpression });
            MethodCallExpression queryExpression = Expression.Call(typeof(Queryable), "Where", new [] { source.ElementType }, new [] { source.Expression, conditionExpression });
            return source.Provider.CreateQuery(queryExpression) as IQueryable<TEntityType>;
        }

        private static Expression CreateVariantContainsExpression(string propertyName, string variant,
                                                                              IEnumerable<string> aliases,
                                                                              ParameterExpression parameterExpression)
        {
            var cExp = CreateContainsExpression(parameterExpression, propertyName, variant);
            if (aliases != null)
            {
                cExp = aliases.Select(alias => CreateContainsExpression(parameterExpression, alias, variant)).Aggregate(cExp, Expression.Or);
            }
            return cExp;
        }

        private static Expression CreateContainsExpression(ParameterExpression parameterExpression, string propertyName, string variant)
        {
            Expression propertyExpression = CreatePropertyExpression(parameterExpression, propertyName);
            Expression containsExpression = null;
            //TODO:  разбиение на слова не работает в постгрессе из-за горбатой lowercase
            string[] words = new[] { variant.ToLower() };//variant.ToLower().Split(new [] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in words)
            {
                ConstantExpression constantExpression = Expression.Constant(word, typeof(string));
                MethodCallExpression tolowerExpression = Expression.Call(propertyExpression, "ToLower", new Type[] { }, null);
                if (containsExpression == null)
                    containsExpression = Expression.Call(tolowerExpression, "Contains", new Type[] { }, constantExpression);
                else
                    containsExpression = Expression.And(containsExpression, Expression.Call(tolowerExpression, "Contains", new Type[] { }, constantExpression));
            }
            return containsExpression;
        }

        /// <summary>
        /// Накладывает фильтр IN (Contains) на свойство типа int. Возможно использование свойства типа EntityObject,
        /// в этом случае автоматически добавляется ".Id".
        /// </summary>
        /// <typeparam name="TEntityType"></typeparam>
        /// <param name="source"></param>
        /// <param name="propertyName">Имя свойства</param>
        /// <param name="values">Набор возможных значений для propertyName</param>
        /// <returns></returns>
        public static IQueryable<TEntityType> WhereContains<TEntityType>(this IQueryable<TEntityType> source, string propertyName, IEnumerable<int> values) where TEntityType : class
        {
            //if (source.ElementType.GetPropertyType(propertyName).IsSubclassOf(typeof(EntityObject)))
            //    propertyName += ".Id";

            if (values == null || !values.Any())
                return source;

            ParameterExpression parameterExpression = Expression.Parameter(source.ElementType, "entity");
            Expression propertyExpression = CreatePropertyExpression(parameterExpression, propertyName);
            var isNullable = propertyExpression.Type.IsGenericType && propertyExpression.Type.GetGenericTypeDefinition() == typeof(Nullable<>);
            var constType = isNullable ? typeof (int?) : typeof (int);
            Expression resultExpression = values.Aggregate<int, Expression>(null, (current, val) => current == null ? Expression.Equal(propertyExpression, Expression.Constant(val, constType)) : Expression.Or(current, Expression.Equal(propertyExpression, Expression.Constant(val, constType))));

            LambdaExpression conditionExpression = Expression.Lambda(resultExpression, new [] { parameterExpression });
            MethodCallExpression queryExpression = Expression.Call(typeof(Queryable), "Where", new [] { source.ElementType }, new [] { source.Expression, conditionExpression });
            return source.Provider.CreateQuery(queryExpression) as IQueryable<TEntityType>;
        }
        /// <summary>
        /// Накладывает фильтр IN (Contains) на свойство типа int. Возможно использование свойства типа EntityObject,
        /// в этом случае автоматически добавляется ".Id".
        /// </summary>
        /// <typeparam name="TEntityType"></typeparam>
        /// <param name="source"></param>
        /// <param name="propertyName">Имя свойства</param>
        /// <param name="values">Набор возможных значений для propertyName</param>
        /// <returns></returns>
        public static IQueryable<TEntityType> WhereContains<TEntityType>(this IQueryable<TEntityType> source, string propertyName, IEnumerable<long> values) where TEntityType : class

        {
            //if (source.ElementType.GetPropertyType(propertyName).IsSubclassOf(typeof(EntityObject)))
            //    propertyName += ".Id";

            if (values == null || !values.Any())
                return source;

            ParameterExpression parameterExpression = Expression.Parameter(source.ElementType, "entity");
            Expression propertyExpression = CreatePropertyExpression(parameterExpression, propertyName);
            var isNullable = propertyExpression.Type.IsGenericType && propertyExpression.Type.GetGenericTypeDefinition() == typeof(Nullable<>);
            var constType = isNullable ? typeof (long?) : typeof (long);
			Expression resultExpression = values.Aggregate<long, Expression>(null, (current, val) => current == null ? Expression.Equal(propertyExpression, Expression.Constant(val, constType)) : Expression.Or(current, Expression.Equal(propertyExpression, Expression.Constant(val, constType))));
			/*
			var contains = typeof( Enumerable ).GetMethods( BindingFlags.Static | BindingFlags.Public ).Single( x => x.Name == "Contains" && x.GetParameters().Length == 2 ).MakeGenericMethod( typeof( long ) );
			var body = Expression.Call( contains, Expression.Constant( values.ToArray() ), propertyExpression );
			Expression resultExpression = body;
			//Expression resultExpression = Expression.Lambda<Func<TEntityType, bool>>( body, propertyExpression );

			//Expression resultExpression = Expression.GreaterThan( Expression.ArrayIndex( Expression.Constant( values ), propertyExpression ), Expression.Constant( -1 ) );
			*/
			LambdaExpression conditionExpression = Expression.Lambda(resultExpression, new [] { parameterExpression });
            MethodCallExpression queryExpression = Expression.Call(typeof(Queryable), "Where", new [] { source.ElementType }, new [] { source.Expression, conditionExpression });
            return source.Provider.CreateQuery(queryExpression) as IQueryable<TEntityType>;
        }

        public static IQueryable<TResultType> Select<TEntityType, TResultType>(this IQueryable<TEntityType> source, string propertyName)
            where TEntityType : class
            where TResultType : class
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(TEntityType), "entity");
            Expression propertyExpression = CreatePropertyExpression(Expression.Convert(parameterExpression, source.ElementType), propertyName);
            var conditionExpression = Expression.Lambda<Func<TEntityType, TResultType>>(Expression.Convert(propertyExpression, typeof(TResultType)), parameterExpression);
            return source.Select(conditionExpression);
        }

        /// <summary>
        /// Накладывает фильтр "больше"
        /// </summary>
        /// <param name="source"></param>
        /// <param name="propertyName">Имя свойства</param>
        /// <param name="value">Параметр, по которому проводится сравнение</param>
        /// <returns></returns>
        public static IQueryable<TEntityType> WhereGreaterThan<TEntityType>(this IQueryable<TEntityType> source, string propertyName, object value) where TEntityType : class
        {
            //if (source.ElementType.GetPropertyType(propertyName).IsSubclassOf(typeof(EntityObject)))
            //    propertyName += ".Name";

            ParameterExpression parameterExpression = Expression.Parameter(source.ElementType, string.Empty);
            Expression propertyExpression = CreatePropertyExpression(parameterExpression, propertyName);
            ConstantExpression constantExpression = Expression.Constant(value, source.ElementType.GetPropertyType(propertyName));
            BinaryExpression greaterExpression = Expression.GreaterThan(propertyExpression, constantExpression);
            LambdaExpression conditionExpression = Expression.Lambda(greaterExpression, new[] { parameterExpression });
            MethodCallExpression queryExpression = Expression.Call(typeof(Queryable), "Where", new[] { source.ElementType }, new[] { source.Expression, conditionExpression });
            return source.Provider.CreateQuery(queryExpression) as IQueryable<TEntityType>;
        }


		/// <summary>
		/// Накладывает фильтр "больше"
		/// </summary>
		/// <param name="source"></param>
		/// <param name="leftProperty">Имя свойства</param>
		/// <param name="leftValue">Параметр, по которому проводится сравнение</param>
		/// <returns></returns>
		public static IQueryable<TEntityType> Intersect<TEntityType>( this IQueryable<TEntityType> source, string leftProperty, object leftValue, string rightProperty, object rightValue ) where TEntityType : class
		{
			//if (source.ElementType.GetPropertyType(propertyName).IsSubclassOf(typeof(EntityObject)))
			//    propertyName += ".Name";

			ParameterExpression parameterExpression = Expression.Parameter( source.ElementType, string.Empty );
			Expression propertyExpression = CreatePropertyExpression( parameterExpression, leftProperty );
			ConstantExpression constantExpression = Expression.Constant( leftValue, source.ElementType.GetPropertyType( leftProperty ) );
			BinaryExpression greaterExpression = Expression.GreaterThan( propertyExpression, constantExpression );

			Expression propertyExpression2 = CreatePropertyExpression( parameterExpression, rightProperty );
			ConstantExpression constantExpression2 = Expression.Constant( rightValue, source.ElementType.GetPropertyType( rightProperty ) );
			BinaryExpression lessExpression = Expression.LessThan( propertyExpression2, constantExpression2 );

			BinaryExpression orExpression = Expression.Or( greaterExpression, lessExpression );
			UnaryExpression notExpression = Expression.Not( orExpression );

			LambdaExpression conditionExpression = Expression.Lambda( notExpression, new[] { parameterExpression } );
			MethodCallExpression queryExpression = Expression.Call( typeof( Queryable ), "Where", new[] { source.ElementType}, new[] { source.Expression, conditionExpression } );
			return source.Provider.CreateQuery( queryExpression ) as IQueryable<TEntityType>;
		}


		/// <summary>
		/// Накладывает фильтр "больше или равно"
		/// </summary>
		/// <param name="source"></param>
		/// <param name="propertyName">Имя свойства</param>
		/// <param name="value">Параметр, по которому проводится сравнение</param>
		/// <returns></returns>
		public static IQueryable<TEntityType> WhereGreaterThanOrEqual<TEntityType>(this IQueryable<TEntityType> source, string propertyName, object value) where TEntityType : class
        {
            //if (source.ElementType.GetPropertyType(propertyName).IsSubclassOf(typeof(EntityObject)))
            //    propertyName += ".Name";

            ParameterExpression parameterExpression = Expression.Parameter(source.ElementType, string.Empty);
            Expression propertyExpression = CreatePropertyExpression(parameterExpression, propertyName);
            ConstantExpression constantExpression = Expression.Constant(value, source.ElementType.GetPropertyType(propertyName));
            BinaryExpression greaterExpression = Expression.GreaterThanOrEqual(propertyExpression, constantExpression);
            LambdaExpression conditionExpression = Expression.Lambda(greaterExpression, new [] { parameterExpression });
            MethodCallExpression queryExpression = Expression.Call(typeof(Queryable), "Where", new [] { source.ElementType }, new [] { source.Expression, conditionExpression });
            return source.Provider.CreateQuery(queryExpression) as IQueryable<TEntityType>;
        }

        /// <summary>
        /// Накладывает фильтр "равно"
        /// </summary>
        /// <param name="source"></param>
        /// <param name="propertyName">Имя свойства</param>
        /// <param name="value">Параметр, по которому проводится сравнение</param>
        /// <returns></returns>
        public static IQueryable<TEntityType> WhereEqual<TEntityType>(this IQueryable<TEntityType> source, string propertyName, object value)
        {
            var propType = source.ElementType.GetPropertyType(propertyName);
            if (value is string && propType != typeof (string))
                value = (value as string).Parse(propType);

            ParameterExpression parameterExpression = Expression.Parameter(source.ElementType, String.Empty);
            Expression propertyExpression = CreatePropertyExpression(parameterExpression, propertyName);
            ConstantExpression constantExpression = Expression.Constant(value, source.ElementType.GetPropertyType(propertyName));
            Expression compareExpression = Expression.Equal(propertyExpression, constantExpression);
            LambdaExpression lambda = Expression.Lambda(compareExpression, parameterExpression);
            MethodCallExpression whereCall = Expression.Call(typeof(Queryable), "Where", new [] { source.ElementType }, source.Expression, Expression.Quote(lambda));
            return source.Provider.CreateQuery(whereCall) as IQueryable<TEntityType>;
        }

        /// <summary>
        /// Накладывает фильтр "не равно"
        /// </summary>
        /// <typeparam name="TEntityType"></typeparam>
        /// <param name="source"></param>
        /// <param name="propertyName">Имя свойства</param>
        /// <param name="value">Параметр, по которому проводится сравнение</param>
        /// <returns></returns>
        public static IQueryable<TEntityType> WhereNotEqual<TEntityType>(this IQueryable<TEntityType> source, string propertyName, object value)
        {
            var propType = source.ElementType.GetPropertyType(propertyName);
            if (value is string && propType != typeof(string))
                value = (value as string).Parse(propType);

            ParameterExpression parameterExpression = Expression.Parameter(source.ElementType, String.Empty);
            Expression propertyExpression = CreatePropertyExpression(parameterExpression, propertyName);
            ConstantExpression constantExpression = Expression.Constant(value, source.ElementType.GetPropertyType(propertyName));
            Expression compareExpression = Expression.NotEqual(propertyExpression, constantExpression);
            LambdaExpression lambda = Expression.Lambda(compareExpression, parameterExpression);
            MethodCallExpression whereCall = Expression.Call(typeof(Queryable), "Where", new[] { source.ElementType }, source.Expression, Expression.Quote(lambda));
            return source.Provider.CreateQuery(whereCall) as IQueryable<TEntityType>;
        }

        /// <summary>
        /// Накладывает фильтр not IsNullOrEmpty. Свойство должно быть только типа string
        /// </summary>
        /// <typeparam name="TEntityType"></typeparam>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static IQueryable<TEntityType> WhereNotEmpty<TEntityType>(this IQueryable<TEntityType> source, string propertyName)
        {
            if (source.ElementType.GetPropertyType(propertyName) != typeof(string))
                throw new ArgumentException("Тип свойства должен быть string");

            ParameterExpression parameterExpression = Expression.Parameter(source.ElementType, String.Empty);
            Expression propertyExpression = CreatePropertyExpression(parameterExpression, propertyName);
            ConstantExpression nullConstantExpression = Expression.Constant(null);
            ConstantExpression emptyConstantExpression = Expression.Constant("", typeof(string));
            BinaryExpression notnullExpression = Expression.NotEqual(propertyExpression, nullConstantExpression);
            BinaryExpression notemptyExpression = Expression.NotEqual(propertyExpression, emptyConstantExpression);
            BinaryExpression andExpression = Expression.And(notnullExpression, notemptyExpression);
            LambdaExpression conditionExpression = Expression.Lambda(andExpression, new [] { parameterExpression });
            MethodCallExpression queryExpression = Expression.Call(typeof(Queryable), "Where", new [] { source.ElementType }, new [] { source.Expression, conditionExpression });
            return source.Provider.CreateQuery(queryExpression) as IQueryable<TEntityType>;
        }

        /// <summary>
        /// Накладывает фильтр IsNullOrEmpty. Свойство должно быть только типа string
        /// </summary>
        /// <typeparam name="TEntityType"></typeparam>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static IQueryable<TEntityType> WhereEmpty<TEntityType>(this IQueryable<TEntityType> source, string propertyName)
        {
            if (source.ElementType.GetPropertyType(propertyName) != typeof(string))
                throw new ArgumentException("Тип свойства должен быть string");

            ParameterExpression parameterExpression = Expression.Parameter(source.ElementType, String.Empty);
            Expression propertyExpression = CreatePropertyExpression(parameterExpression, propertyName);
            ConstantExpression nullConstantExpression = Expression.Constant(null);
            ConstantExpression emptyConstantExpression = Expression.Constant("", typeof(string));
            BinaryExpression nullExpression = Expression.Equal(propertyExpression, nullConstantExpression);
            BinaryExpression emptyExpression = Expression.Equal(propertyExpression, emptyConstantExpression);
            BinaryExpression orExpression = Expression.Or(nullExpression, emptyExpression);
            LambdaExpression conditionExpression = Expression.Lambda(orExpression, new [] { parameterExpression });
            MethodCallExpression queryExpression = Expression.Call(typeof(Queryable), "Where", new [] { source.ElementType }, new [] { source.Expression, conditionExpression });
            return source.Provider.CreateQuery(queryExpression) as IQueryable<TEntityType>;
        }

        /// <summary>
        /// Накладывает фильтр "меньше или равно" на свойство типа string
        /// </summary>
        /// <param name="source"></param>
        /// <param name="propertyName">Имя свойства</param>
        /// <param name="value">Параметр, по которому проводится сравнение</param>
        /// <returns></returns>
        public static IQueryable<TEntityType> WhereLessThanOrEqual<TEntityType>(this IQueryable<TEntityType> source, string propertyName, object value) where TEntityType : class
        {
            //if (source.ElementType.GetPropertyType(propertyName).IsSubclassOf(typeof(EntityObject)))
            //    propertyName += ".Name";

            ParameterExpression parameterExpression = Expression.Parameter(source.ElementType, string.Empty);
            Expression propertyExpression = CreatePropertyExpression(parameterExpression, propertyName);
            ConstantExpression constantExpression = Expression.Constant(value, source.ElementType.GetPropertyType(propertyName));
            BinaryExpression lessExpression = Expression.LessThanOrEqual(propertyExpression, constantExpression);
            LambdaExpression conditionExpression = Expression.Lambda(lessExpression, new [] { parameterExpression });
            MethodCallExpression queryExpression = Expression.Call(typeof(Queryable), "Where", new [] { source.ElementType }, new [] { source.Expression, conditionExpression });
            return source.Provider.CreateQuery(queryExpression) as IQueryable<TEntityType>;
        }

        public static IQueryable<TEntityType> WhereLessThan<TEntityType>(this IQueryable<TEntityType> source, string propertyName, object value) where TEntityType : class
        {
            //if (source.ElementType.GetPropertyType(propertyName).IsSubclassOf(typeof(EntityObject)))
            //    propertyName += ".Name";

            ParameterExpression parameterExpression = Expression.Parameter(source.ElementType, string.Empty);
            Expression propertyExpression = CreatePropertyExpression(parameterExpression, propertyName);
            ConstantExpression constantExpression = Expression.Constant(value, source.ElementType.GetPropertyType(propertyName));
            BinaryExpression lessExpression = Expression.LessThan(propertyExpression, constantExpression);
            LambdaExpression conditionExpression = Expression.Lambda(lessExpression, new [] { parameterExpression });
            MethodCallExpression queryExpression = Expression.Call(typeof(Queryable), "Where", new [] { source.ElementType }, new [] { source.Expression, conditionExpression });
            return source.Provider.CreateQuery(queryExpression) as IQueryable<TEntityType>;
        }

        public static IQueryable<TEntityType> WhereNullOrLessThan<TEntityType>(this IQueryable<TEntityType> source, string propertyName, object value) where TEntityType : class
        {
            //if (source.ElementType.GetPropertyType(propertyName).IsSubclassOf(typeof(EntityObject)))
            //    propertyName += ".Name";

            ParameterExpression parameterExpression = Expression.Parameter(source.ElementType, string.Empty);
            Expression propertyExpression = CreatePropertyExpression(parameterExpression, propertyName);
            ConstantExpression nullExpression = Expression.Constant(null);
            ConstantExpression constantExpression = Expression.Constant(value, source.ElementType.GetPropertyType(propertyName));
            BinaryExpression nullEqualsExpression = Expression.Equal(propertyExpression, nullExpression);
            BinaryExpression lessExpression = Expression.LessThan(propertyExpression, constantExpression);
            BinaryExpression nullOrLessExpression = Expression.Or(nullEqualsExpression, lessExpression);
            LambdaExpression conditionExpression = Expression.Lambda(nullOrLessExpression, new [] { parameterExpression });
            MethodCallExpression queryExpression = Expression.Call(typeof(Queryable), "Where", new [] { source.ElementType }, new [] { source.Expression, conditionExpression });
            return source.Provider.CreateQuery(queryExpression) as IQueryable<TEntityType>;
        }

        public static IQueryable<TEntityType> WhereNullOrGreaterThan<TEntityType>(this IQueryable<TEntityType> source, string propertyName, object value) where TEntityType : class
        {
            //if (source.ElementType.GetPropertyType(propertyName).IsSubclassOf(typeof(EntityObject)))
            //    propertyName += ".Name";

            ParameterExpression parameterExpression = Expression.Parameter(source.ElementType, string.Empty);
            Expression propertyExpression = CreatePropertyExpression(parameterExpression, propertyName);
            ConstantExpression nullExpression = Expression.Constant(null);
            ConstantExpression constantExpression = Expression.Constant(value, source.ElementType.GetPropertyType(propertyName));
            BinaryExpression nullEqualsExpression = Expression.Equal(propertyExpression, nullExpression);
            BinaryExpression greaterExpression = Expression.GreaterThan(propertyExpression, constantExpression);
            BinaryExpression nullOrGreaterExpression = Expression.Or(nullEqualsExpression, greaterExpression);
            LambdaExpression conditionExpression = Expression.Lambda(nullOrGreaterExpression, new [] { parameterExpression });
            MethodCallExpression queryExpression = Expression.Call(typeof(Queryable), "Where", new [] { source.ElementType }, new [] { source.Expression, conditionExpression });
            return source.Provider.CreateQuery(queryExpression) as IQueryable<TEntityType>;
        }

        /// <summary>
        /// Накладывает фильтр по дате для запросов, тип которых реализует интерфейс IDataDependent
        /// </summary>
        /// <typeparam name="TEntityType">тип запроса</typeparam>
        /// <param name="source"></param>
        /// <param name="date">дата для фильтра</param>
        /// <returns></returns>
        //public static IQueryable<TEntityType> ForDate<TEntityType>(this IQueryable<TEntityType> source, DateTime date) where TEntityType : IDateDependent
        //{
        //    ParameterExpression parameterExpression = Expression.Parameter(source.ElementType, string.Empty);
        //    Expression beginPropExpression = CreatePropertyExpression(parameterExpression, "BeginDate");
        //    Expression endPropExpression = CreatePropertyExpression(parameterExpression, "EndDate");
        //    ConstantExpression nullExpression = Expression.Constant(null);
        //    ConstantExpression constantExpression = Expression.Constant(date, typeof(DateTime?));
        //    BinaryExpression beginNullExpression = Expression.Equal(beginPropExpression, nullExpression);
        //    BinaryExpression endNullExpression = Expression.Equal(endPropExpression, nullExpression);
        //    BinaryExpression beginLessExpression = Expression.LessThanOrEqual(beginPropExpression, constantExpression);
        //    BinaryExpression endGreatExpression = Expression.GreaterThanOrEqual(endPropExpression, constantExpression);
        //    BinaryExpression beginNullOrLessExpression = Expression.Or(beginNullExpression, beginLessExpression);
        //    BinaryExpression endNullOrGreatExpression = Expression.Or(endNullExpression, endGreatExpression);
        //    BinaryExpression dateFullExpression = Expression.And(beginNullOrLessExpression, endNullOrGreatExpression);
        //    LambdaExpression conditionExpression = Expression.Lambda(dateFullExpression, new[] { parameterExpression });
        //    MethodCallExpression queryExpression = Expression.Call(typeof(Queryable), "Where", new[] { source.ElementType }, new[] { source.Expression, conditionExpression });
        //    return source.Provider.CreateQuery(queryExpression) as IQueryable<TEntityType>;
        //}

        /// <summary>
        /// Накладывает фильтр сортировки по возрастанию
        /// </summary>
        /// <param name="source"></param>
        /// <param name="propertyName">Поле сортировки</param>
        /// <returns></returns>
        public static IQueryable<TEntityType> OrderBy<TEntityType>(this IQueryable<TEntityType> source, string propertyName) where TEntityType : class
        {
            //if (source.ElementType.GetPropertyType(propertyName).IsSubclassOf(typeof(EntityObject)))
            //    propertyName += ".Name";

            ParameterExpression parameter = Expression.Parameter(source.ElementType, "o");
            Expression member = CreatePropertyExpression(parameter, propertyName);
            LambdaExpression lambda = Expression.Lambda(member, parameter);
            MethodCallExpression orderByCallExpression = Expression.Call(
                            typeof(Queryable),
                            "OrderBy",
                            new [] { source.ElementType, member.Type },
                            source.Expression,
                            Expression.Quote(lambda));

            return source.Provider.CreateQuery(orderByCallExpression) as IQueryable<TEntityType>;
        }

        public static IQueryable<TEntityType> ThenBy<TEntityType>(this IQueryable<TEntityType> source, string propertyName) where TEntityType : class
        {
            //if (source.ElementType.GetPropertyType(propertyName).IsSubclassOf(typeof(EntityObject)))
            //    propertyName += ".Name";

            ParameterExpression parameter = Expression.Parameter(source.ElementType, "o");
            Expression member = CreatePropertyExpression(parameter, propertyName);
            LambdaExpression lambda = Expression.Lambda(member, parameter);
            MethodCallExpression orderByCallExpression = Expression.Call(
                            typeof(Queryable),
                            "ThenBy",
                            new[] { source.ElementType, member.Type },
                            source.Expression,
                            Expression.Quote(lambda));

            return source.Provider.CreateQuery(orderByCallExpression) as IQueryable<TEntityType>;
        }

        /// <summary>
        /// Накладывает фильтр сортировки по убыванию
        /// </summary>
        /// <param name="source"></param>
        /// <param name="propertyName">Поле сортировки</param>
        /// <returns></returns>
        public static IQueryable<TEntityType> OrderByDescending<TEntityType>(this IQueryable<TEntityType> source, string propertyName) where TEntityType : class
        {
            //if (source.ElementType.GetPropertyType(propertyName).IsSubclassOf(typeof(EntityObject)))
            //    propertyName += ".Name";

            ParameterExpression parameter = Expression.Parameter(source.ElementType, "o");
            Expression member = CreatePropertyExpression(parameter, propertyName);
            LambdaExpression lambda = Expression.Lambda(member, parameter);
            MethodCallExpression orderByCallExpression = Expression.Call(
                            typeof(Queryable),
                            "OrderByDescending",
                            new [] { source.ElementType, member.Type },
                            source.Expression,
                            Expression.Quote(lambda));

            return source.Provider.CreateQuery(orderByCallExpression) as IQueryable<TEntityType>;
        }

        public static IQueryable<TEntityType> ThenByDescending<TEntityType>(this IQueryable<TEntityType> source, string propertyName) where TEntityType : class
        {
            //if (source.ElementType.GetPropertyType(propertyName).IsSubclassOf(typeof(EntityObject)))
            //    propertyName += ".Name";

            ParameterExpression parameter = Expression.Parameter(source.ElementType, "o");
            Expression member = CreatePropertyExpression(parameter, propertyName);
            LambdaExpression lambda = Expression.Lambda(member, parameter);
            MethodCallExpression orderByCallExpression = Expression.Call(
                            typeof(Queryable),
                            "ThenByDescending",
                            new[] { source.ElementType, member.Type },
                            source.Expression,
                            Expression.Quote(lambda));

            return source.Provider.CreateQuery(orderByCallExpression) as IQueryable<TEntityType>;
        }

        private static Expression CreatePropertyExpression(Expression parameterExpression, string propertyName)
        {
            return propertyName.Split('.').Aggregate<string, Expression>(null, (current, str) => Expression.PropertyOrField(current ?? parameterExpression, str));
        }
    }
}
