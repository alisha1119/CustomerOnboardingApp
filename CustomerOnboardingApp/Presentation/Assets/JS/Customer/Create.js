//Click on the add button to add new director/shareholder
let directorIndex = 0;
document.getElementById('addDirectorShareholder').addEventListener('click', function () {
    var container = document.getElementById('directorShareholderContainer');

    // Create a new section for the director/shareholder
    var newSection = document.createElement('div');
    newSection.className = 'director-shareholder';
    newSection.innerHTML = `
                        <select name="DirectorShareholderList[${directorIndex}].Title">
                        <option value="">Select Title*</option>
                        <option value="Mr">Mr</option>
                        <option value="Ms">Ms</option>
                        <option value="Mrs">Mrs</option>
                        <option value="Dr">Dr</option>
                        </select>
                    <input type="text" name="DirectorShareholderList[${directorIndex}].FirstName" placeholder="First Name*" >
                    <input type="text" name="DirectorShareholderList[${directorIndex}].LastName" placeholder="Last Name*" >
                    <input type="text" name="DirectorShareholderList[${directorIndex}].PassportNumber" placeholder="Passport Number*" >
                    <select name="DirectorShareholderList[${directorIndex}].Role">
                        <option value="">Select Role</option>
                        <option value="Director">Director</option>
                        <option value="Shareholder">Shareholder</option>
                     </select>
                    <button type="button" class="remove-button">&times;</button> 
                    `;
    container.appendChild(newSection);
    directorIndex++;
});

// Validate the form before submitting
document.getElementById('customerForm').addEventListener('submit', function (event) {
    let isValid = true;
    //Make sure the div displaying the errors are empty
    document.getElementById('errorMessagesCustomer').innerHTML = "";
    document.getElementById('errorMessagesDirectorShareholder').innerHTML = "";

    let errorMessagesCustomer = [];
    let errorMessagesDirectorShareholder = [];
    let errorMessagesDocument = [];

    let isValidCustomer = ValidateCustomerFields(errorMessagesCustomer);
    isValidCustomer = ValidateDocuments(errorMessagesCustomer);
    if (!isValidCustomer) {
        const errorContainer = document.getElementById('errorMessagesCustomer');
        errorContainer.innerHTML = errorMessagesCustomer.join('<br>');
    }

    let isValidDirectorShareholder = ValidateDirectorShareholder(errorMessagesDirectorShareholder);
    if (!isValidDirectorShareholder) {
        const errorContainer = document.getElementById('errorMessagesDirectorShareholder');
        errorContainer.innerHTML = errorMessagesDirectorShareholder.join('<br>');
    }

    

    if (!isValidDirectorShareholder && isValidCustomer) {
        document.getElementById('errorMessagesDirectorShareholder').scrollIntoView({ behavior: 'smooth' });
    }
    if (!isValidCustomer) {
        document.getElementById('errorMessagesCustomer').scrollIntoView({ behavior: 'smooth' });
    }

    isValid = isValidCustomer && isValidDirectorShareholder;

    if (!isValid) {
        event.preventDefault(); // Prevent form submission if validation fails
    }
});

// Remove the director/shareholder row
document.getElementById('directorShareholderContainer').addEventListener('click', function (event) {
    if (event.target.classList.contains('remove-button')) {
        event.target.parentElement.remove(); 
    }
});

//Validate the customer fields
function ValidateCustomerFields(errorMessagesCustomer) {
    let isValid = true;
    if (!document.getElementById('mainPurposeId').value) {
        errorMessagesCustomer.push('Please select a main purpose.');
        isValid = false;
    }
    if (!document.getElementById('nameOfCompany').value) {
        errorMessagesCustomer.push('Please add a company name.');
        isValid = false;
    }
    if (!document.getElementById('typeOfEntityId').value) {
        errorMessagesCustomer.push('Please select a type of entity.');
        isValid = false;
    }
    if (!document.getElementById('businessActivityId').value) {
        errorMessagesCustomer.push('Please select a business activity.');
        isValid = false;
    }
    if (!document.getElementById('countryId').value) {
        errorMessagesCustomer.push('Please select a country.');
        isValid = false;
    }
    if (!document.getElementById('registrationNumber').value) {
        errorMessagesCustomer.push('Please add a registration number.');
        isValid = false;
    }
    if (!document.getElementById('dateOfIncorporation').value) {
        errorMessagesCustomer.push('Please select a date of incorporation.');
        isValid = false;
    }
    if (!document.getElementById('titleDesignateApplicant').value) {
        errorMessagesCustomer.push('Please select a title for the designated applicant.');
        isValid = false;
    }
    if (!document.getElementById('firstNameDesignateApplicant').value) {
        errorMessagesCustomer.push('Please add a first name for the designated applicant.');
        isValid = false;
    }
    if (!document.getElementById('lastNameDesignateApplicant').value) {
        errorMessagesCustomer.push('Please add a last name for the designated applicant.');
        isValid = false;
    }

     isValid = ValidateEmail(errorMessagesCustomer);
    return isValid;
}

//Validate the email field
function ValidateEmail(errorMessagesCustomer) {
    let isValid = true;
    const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    const email = document.getElementById('emailAddress').value;
    if (!email) {
        errorMessagesCustomer.push('Please add an email address.');
        isValid = false;
    } else {
        if (!regex.test(email)) {
            errorMessagesCustomer.push('Invalid email address.');
            isValid = false;
        }
        return isValid;
    }
}

//Validate the fields of the director/shareholder
function ValidateDirectorShareholder(errorMessagesDirectorShareholder) {
    let isValid = true;
    // Select all dynamically added fields
    const sections = document.querySelectorAll('#directorShareholderContainer .director-shareholder');

    sections.forEach(section => {
        const title = section.querySelector('select[name$=".Title"]').value;
        const firstName = section.querySelector('input[name$=".FirstName"]').value;
        const lastName = section.querySelector('input[name$=".LastName"]').value;
        const passportNumber = section.querySelector('input[name$=".PassportNumber"]').value;

        // Validate fields
        if (!title) {
            errorMessagesDirectorShareholder.push('Please select a title for all directors/shareholders.');
            isValid = false;
        }
        if (!firstName) {
            errorMessagesDirectorShareholder.push('Please enter a first name for all directors/shareholders.');
            isValid = false;
        }
        if (!lastName) {
            errorMessagesDirectorShareholder.push('Please enter a last name for all directors/shareholders.');
            isValid = false;
        }
        if (!passportNumber) {
            errorMessagesDirectorShareholder.push('Please enter a passport number for all directors/shareholders.');
            isValid = false;
        }
    });
    return isValid;
};

//Validate the required documents and check size and file extension allowed
function ValidateDocuments(errorMessagesCustomer) {
    let isValid = true;
    var allowedExtensions = ['.pdf', '.jpg', '.png', '.jpeg'];
    var maxFileSize = 10485760; // 10MB
    var documentIdentityCard = document.getElementById("documentIdentityCard");
    var documentCertificate = document.getElementById("documentCertificate");

    if (documentIdentityCard.files.length === 0) {
        errorMessagesCustomer.push('Please upload a copy of your identity card.');
        isValid = false;
    } else {
            var file = documentIdentityCard.files[0];
            var fileSize = file.size;
            var fileName = file.name;
            var fileExtension = fileName.slice(((fileName.lastIndexOf(".") - 1) >>> 0) + 2).toLowerCase();

            if (fileSize > maxFileSize) {
                errorMessagesCustomer.push('The size of file ' + fileName + ' is greater than 10MB.');
                isValid = false;
            } else if (!allowedExtensions.includes('.' + fileExtension)) {
                errorMessagesCustomer.push('The extension of file ' + fileName + ' is not allowed.');
                isValid = false; 
            }
        }

    if (documentCertificate.files.length === 0) {
        errorMessagesCustomer.push('Please upload a copy of your certificate.');
        isValid = false;
    } else {
        var file = documentCertificate.files[0];
        var fileSize = file.size;
        var fileName = file.name;
        var fileExtension = fileName.slice(((fileName.lastIndexOf(".") - 1) >>> 0) + 2).toLowerCase();

        if (fileSize > maxFileSize) {
            errorMessagesCustomer.push('The size of file ' + fileName + ' is greater than 10MB.');
            isValid = false;
        } else if (!allowedExtensions.includes('.' + fileExtension)) {
            errorMessagesCustomer.push('The extension of file ' + fileName + ' is not allowed.');
            isValid = false; 
        }
    }

    return isValid;
}
var licenseRequirementsContainer = document.getElementById('licenseRequirementsContainer');

//Display the field licence requirements if the business activity is banking (Id =1)
document.addEventListener('DOMContentLoaded', function () {
    var businessActivityDropdown = document.getElementById('businessActivityId');
    var licenseRequirementsInput = document.getElementById('licenseRequirements');

    function toggleLicenseRequirements() {
        if (businessActivityDropdown.value == '1') { 
            licenseRequirementsInput.parentElement.style.display = 'block';
        } else {
            licenseRequirementsInput.parentElement.style.display = 'none';
        }
    }

    toggleLicenseRequirements();

    businessActivityDropdown.addEventListener('change', toggleLicenseRequirements);
});

// Set the current date as the value of the date input
document.addEventListener('DOMContentLoaded', function () {
    var dateInput = document.getElementById('dateOfIncorporation');

    
    var today = new Date();
    var year = today.getFullYear();
    var month = ('0' + (today.getMonth() + 1)).slice(-2);
    var day = ('0' + today.getDate()).slice(-2);

    dateInput.value = year + '-' + month + '-' + day;
});