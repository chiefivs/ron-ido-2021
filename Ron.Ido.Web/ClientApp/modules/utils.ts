export namespace Utils {
    /**
     * Возвращает шаблон в виде массива html-элементов из файла.
     * Пример: const nodes = Template.getNodesFromFile('app.html');
     * @param path - путь к файлу начиная с папки templates
     */
    export function getNodesFromFile(path: string): Element[] {
        const module = require(`@templates/${path}`);
        const container = $('<div>').html(module.default);
        return container.children().toArray();
    }

    /**
     * Возвращает шаблон в виде массива html-элементов из строки.
     * Пример: const nodes = Template.getNodesFromHtml('<div>any content</div>');
     * @param html - текст (html-разметка)
     */
    export function getNodesFromHtml(html: string): Element[] {
        return $(html).toArray();
    }

    /**
     * Функция animate вызывается в течение установленного интервала времени 
     * с равным шагом от from до to (анимация)
     * @param action - функция анимации
     * @param from - начальное значение
     * @param to - конечное значение
     */
    export function animate(action:(val:number) => void, from:number, to: number){
        const stepsCnt = 20;
        const duration = 300;
        const step = (to - from) / stepsCnt;

        let num = 0;
        const interval = setInterval(() => {
            num++;
            const next = from + step * num;

            if(num < stepsCnt) {
                action(next);
            } else {
                action(to);
                clearInterval(interval);
            }
        }, duration / stepsCnt);
    }
} 
