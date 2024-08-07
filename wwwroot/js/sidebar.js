document.addEventListener('DOMContentLoaded', function () {
    var sidebarMenu = document.getElementById('sidebar-menu');
    var menuItems = sidebarMenu.getElementsByClassName('nav-link');

    for (var i = 0; i < menuItems.length; i++) {
        menuItems[i].addEventListener('click', function () {
            var current = sidebarMenu.getElementsByClassName('active');
            current[0].classList.remove('active');
            this.classList.add('active');
        });
    }
});