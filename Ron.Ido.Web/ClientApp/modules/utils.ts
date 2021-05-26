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
     * Возвращает шаблон в виде массива html-элементов из тега script по идентификатору.
     * Пример: <script type="text/html" id="template-id"><div>template nodes</div></script>
     * const nodes = Template.getNodesFromScriptElement('template-id');
     * вернет <div>template nodes</div>
     * @param id - идентификатор тега script
     */
    export function getNodesFromScriptElement(id: string): Element[] {
        return getNodesFromHtml($(`script#${id}`).html());
    }

    /**
     * Функция animate вызывается в течение установленного интервала времени 
     * с равным шагом от from до to (анимация)
     * @param from - начальное значение
     * @param to - конечное значение
     * @param action - функция анимации
     */
    export function animate(from:number, to: number, action:(val:number) => void, after?: () => void){
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
                if(after)
                    after();
            }
        }, duration / stepsCnt);
    }

    export function getElementRect(element:JQuery):DOMRect {
        let rect:DOMRect;
        const style = element.attr('style');
        element.css('position', 'absolute').css('visibility', 'hidden').css('display', 'block').css('height', '');
        rect = element[0].getBoundingClientRect();
        element.attr('style', style);

        return rect;
    }

    export function randomString(length) {
        var chars = '0123456789ABCDEFGHIJKLMNOPQRSTUVWXTZabcdefghiklmnopqrstuvwxyz'.split('');
    
        if (! length) {
            length = Math.floor(Math.random() * chars.length);
        }
    
        var str = '';
        for (var i = 0; i < length; i++) {
            str += chars[Math.floor(Math.random() * chars.length)];
        }
        return str;
    }
} 
