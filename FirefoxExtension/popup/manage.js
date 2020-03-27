$(document).ready(() => {
    $('#scoreSlider').slider({
        min: 0,
        max: 1,
        step: 0.01,
        interpretLabel: value => {
            if(value == 100) {
                $('.halftick.label').remove();
            }
            return value/100;
        },
        onChange: () => {
            browser.storage.sync.set({
                minScore: $('#scoreSlider').slider('get value')
            });
        }
    });
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
    let minScore = browser.storage.sync.get("minScore");
    getting.then(result => {
        $('#enabledCheckbox').prop('checked', (result.enabled != undefined) ? result.enabled : true);
        updateStatusText();
    }, error => {
        console.log("Error: " + error);
    });
    minScore.then(result => {
        $('#scoreSlider').slider('set value', result.minScore);
    })
}

function updateStatusText() {
    $('#status').html("Adfinder is " + ($('#enabledCheckbox').prop('checked') ? "<b>enabled</b>" : "<b>disabled</b>"));
}