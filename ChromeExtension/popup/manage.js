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
            chrome.storage.sync.set({
                minScore: $('#scoreSlider').slider('get value')
            });
        }
    });
    restoreOptions();
    $('#enabledCheckbox').change(() => {
        chrome.storage.sync.set({
            enabled: $('#enabledCheckbox').prop('checked')
        });
        updateStatusText();
    });
});

function restoreOptions() {
    let getting = chrome.storage.sync.get("enabled", result => {
        $('#enabledCheckbox').prop('checked', (result.enabled != undefined) ? result.enabled : true);
        updateStatusText();
    });
    let minScore = chrome.storage.sync.get("minScore", result => {
        $('#scoreSlider').slider('set value', (!isNaN(result.minScore)) ? result.minScore : 0.75);
    })
}

function updateStatusText() {
    $('#status').html("Adfinder is " + ($('#enabledCheckbox').prop('checked') ? "<b>enabled</b>" : "<b>disabled</b>"));
}