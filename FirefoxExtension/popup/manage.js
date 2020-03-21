$(document).ready(() => {
    restoreOptions();
    $('#enabledCheckbox').change(() => {
        browser.storage.sync.set({
            enabled: $('#enabledCheckbox').prop('checked')
        });
        updateStatusText();
    });
});

function restoreOptions() {
    let getting = browser.storage.sync.get("enabled");
    getting.then(result => {
        $('#enabledCheckbox').prop('checked', (result.enabled != undefined) ? result.enabled : true);
        updateStatusText();
    }, error => {
        console.log("Error: " + error);
    });
}

function updateStatusText() {
    $('#status').html("Adfinder is " + ($('#enabledCheckbox').prop('checked') ? "<b>enabled</b>" : "<b>disabled</b>"));
}