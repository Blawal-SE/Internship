function fieldRequired(value) {
    return value != "";
}

function isAplhabet(value) {

    return /^[a-zA-Z ]{2,30}$/.test(value);
}

function isEmail(value) {
    return /[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}/.test(value);
}

function isEqual(value1, value2) {
    return value1 == value2;
}