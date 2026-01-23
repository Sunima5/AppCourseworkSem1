window.createQuill = (id, content) => {
    const el = document.getElementById(id);
    if (!el) return;

    el.innerHTML = "";

    const quill = new Quill(el, {
        theme: "snow",
        modules: {
            toolbar: [
                [{ header: [1, 2, false] }],
                ["bold", "italic", "underline"],
                [{ list: "ordered" }, { list: "bullet" }],
                ["link"],
                ["clean"]
            ]
        }
    });

    quill.root.innerHTML = content ?? "";
    window.__quill = quill;
};

window.getQuillContent = () => {
    return window.__quill?.root.innerHTML ?? "";
};
