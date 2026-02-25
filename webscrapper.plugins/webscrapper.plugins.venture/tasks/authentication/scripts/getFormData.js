const formData = {};
const inputs = document.querySelectorAll('input');

for (var i = 0; i < inputs.length; i++) {
    const el = inputs[i];

    if (el.name) {
        formData[el.name] = el.value || "";
    }
}

JSON.stringify(formData);

