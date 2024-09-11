//Validate the form before submitting it
document.getElementById('loginForm').addEventListener('submit', function (event) {
    let isValid = true;
    let errorMessages = [];

    if (!document.getElementById('password').value) {
        errorMessages.push('Please enter a password.');
        isValid = false;
    }
    isValid = ValidateEmail(errorMessages);

    if (!isValid) {
        const errorContainer = document.getElementById('errorMessages');
        errorContainer.innerHTML = errorMessages.join('<br>');
        event.preventDefault(); // Prevent form submission if validation fails
    }
});

//Function validating the email address
function ValidateEmail(errorMessages) {
    let isValid = true;
    const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    const email = document.getElementById('email').value;
    if (!email) {
        errorMessages.push('Please add an email address.');
        isValid = false;
    } else {
        if (!regex.test(email)) {
            errorMessages.push('Invalid email address.');
            isValid = false;
        }
        return isValid;
    }
}