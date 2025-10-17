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

// System theme detection
window.getSystemThemePreference = function () {
    return window.matchMedia('(prefers-color-scheme: dark)').matches;
};

// Listen for system theme changes
window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', function (e) {
    // You can optionally trigger theme updates when system preference changes
    console.log('System theme changed to:', e.matches ? 'dark' : 'light');
});
