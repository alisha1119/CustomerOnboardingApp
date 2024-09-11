
document.addEventListener('DOMContentLoaded', function () {
    // Handle process button click
    var approveButton = document.getElementById('process');
    if (approveButton) {
        approveButton.addEventListener('click', function () {
            var customerId = document.getElementById('customerId').value;
            if (customerId) {
                window.location.href = '/Dashboard/ProcessCustomer?customerId=' + customerId;
            }
        });
    }

    // Handle reject button click
    var rejectButton = document.getElementById('rejectSubmitted');
    if (rejectButton) {
        rejectButton.addEventListener('click', function () {
            var customerId = document.getElementById('customerId').value;
            if (customerId) {
                window.location.href = '/Dashboard/rejectSubmittedCustomer?customerId=' + customerId;
            }
        });
    }

    // Handle approve button click
    var approveButton = document.getElementById('approve');
    if (approveButton) {
        approveButton.addEventListener('click', function () {
            var customerId = document.getElementById('customerId').value;
            if (customerId) {
                window.location.href = '/Dashboard/ApproveCustomer?customerId=' + customerId;
            }
        });
    }

    // Handle reject button click for processed applications
    var approveButton = document.getElementById('rejectProcessed');
    if (approveButton) {
        approveButton.addEventListener('click', function () {
            var customerId = document.getElementById('customerId').value;
            if (customerId) {
                window.location.href = '/Dashboard/RejectProcessedCustomer?customerId=' + customerId;
            }
        });
    }
});