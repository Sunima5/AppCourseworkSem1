window.editor = {
    exec: function (command, value = null) {
        document.execCommand(command, false, value);
    },

    getContent: function () {
        return document.getElementById("editor").innerHTML;
    },

    setContent: function (html) {
        document.getElementById("editor").innerHTML = html;
    }
};
