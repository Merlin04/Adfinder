chrome.tabs.onUpdated.addListener((tabId, changeInfo, tab) => {
    if(tab.status == "complete" && tab.active) {
        let getting = chrome.storage.sync.get("enabled", result => {
            let minScore = chrome.storage.sync.get("minScore", result2 => {
                if ((result.enabled != undefined) ? result.enabled : true) {
                    chrome.tabs.sendMessage(tabId, {"message": "checkPopup", "minScore": (!isNaN(result2.minScore)) ? result2.minScore : 0.75});
                }
            });
        });
    }
});