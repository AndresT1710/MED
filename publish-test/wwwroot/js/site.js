window.setCheckboxState = (element, checked) => {
    if (element && element.type === 'checkbox') {
        element.checked = checked;
    }
};