const login = document.getElementById('login');
const register = document.getElementById('register');
const activeColor = '#2E62D9';

const loadHandler = () => {
    const currentPath = document.location.pathname.toLowerCase();
    if (currentPath === '/account/login') {
        login.style.color = activeColor;
    } else {
        register.style.color = activeColor;
    }
};

document.addEventListener('DOMContentLoaded', loadHandler);
