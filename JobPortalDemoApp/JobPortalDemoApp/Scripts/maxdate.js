$.validator.unobtrusive.adapters.addSingleVal("maxdate", "currentdate");
$.validator.addMethod("maxdate", function (value, element, currentdate) {
    if (value) {
        alert(value);
        alert(currentdate);
        if (value < currentdate) {
            return true;
        }
        return false;
    }
    return true;
   
}); 