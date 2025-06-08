document.addEventListener("DOMContentLoaded", function () {
    const toggleBtn = document.getElementById("toggle-dark-mode");

    function enableDarkMode() {
        document.body.classList.add("dark-mode");
        localStorage.setItem("darkMode", "enabled");
        if (toggleBtn) toggleBtn.innerHTML = '<i class="fas fa-sun"></i>';
    }

    function disableDarkMode() {
        document.body.classList.remove("dark-mode");
        localStorage.setItem("darkMode", "disabled");
        if (toggleBtn) toggleBtn.innerHTML = '<i class="fas fa-moon"></i>';
    }

    if (localStorage.getItem("darkMode") === "enabled") {
        enableDarkMode();
    } else {
        disableDarkMode();
    }

    if (toggleBtn) {
        toggleBtn.addEventListener("click", () => {
            if (document.body.classList.contains("dark-mode")) {
                disableDarkMode();
            } else {
                enableDarkMode();
            }
        });
    }
});