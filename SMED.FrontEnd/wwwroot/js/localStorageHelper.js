window.localStorageHelper = {
    savePerson: function (data) {
        localStorage.setItem("personData", data);
    },
    loadPerson: function () {
        return localStorage.getItem("personData");
    },
    clearPerson: function () {
        localStorage.removeItem("personData");
    }
};
