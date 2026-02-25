(function () {
    var formData = {};
    var inputs = document.querySelectorAll('input');
    
    for (var i = 0; i < inputs.length; i++) {
        var el = inputs[i];
        if (el.name) {
            formData[el.name] = el.value || "";
        }
    }
    
    return JSON.stringify(formData);
})();
