$.validator.unobtrusive.adapters.addSingleVal("maxdate", "currentdate");
$.validator.addMethod("maxdate", function (value, element, currentdate) {
   
        alert(value);
        alert(currentdate);
        if (value < currentdate) {
            return true;
        }
        return false;
   
}); 