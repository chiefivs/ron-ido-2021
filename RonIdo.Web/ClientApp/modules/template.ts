export default class Template {
    static getNodes = (name: string): Element[] => {
        const module = require(`@templates/${name}`);
        const container = $('<div>').html(module.default);
        return container.toArray();
    }
} 