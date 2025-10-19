window.tabStorage = {
    set: function (key, value) {
        localStorage.setItem(key, value);
    },
    get: function (key) {
        return localStorage.getItem(key);
    }
};

window.themeStorage = {
    set: function (value) {
        localStorage.setItem("apexlm-theme", value);
    },
    get: function () {
        return localStorage.getItem("apexlm-theme");
    }
};
