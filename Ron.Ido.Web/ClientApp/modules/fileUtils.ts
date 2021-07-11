export namespace FileUtils {
    /**
     * Загружает строку base64 как файл
     */
    export function download(base64:string, name:string, contentType:string) {
        if(!base64)
            return;

        var bytechars = atob(base64);
        var slicesize = slicesize || 512;
        var bytearrays = [];
        for (var offset = 0; offset < bytechars.length; offset += slicesize) {
            var slice = bytechars.slice(offset, offset + slicesize);
            var bytenums = new Array(slice.length);
            for (var i = 0; i < slice.length; i++) {
                bytenums[i] = slice.charCodeAt(i);
            }
            var bytearray = new Uint8Array(bytenums);
            bytearrays[bytearrays.length] = bytearray;
        }

        const blob = new Blob(bytearrays, {type:contentType});
        const url = window.URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url;
        a.download = name;
        a.click();
        window.URL.revokeObjectURL(url);
    }
}