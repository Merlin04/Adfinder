browser.tabs.onUpdated.addListener((tabId, changeInfo, tab) => {
    if(tab.status == "complete" && tab.active) {
        let getting = browser.storage.sync.get("enabled");
        getting.then(result => {
            let minScore = browser.storage.sync.get("minScore");
            minScore.then(result2 => {
                if ((result.enabled != undefined) ? result.enabled : true) {
                    browser.tabs.sendMessage(tabId, {"message": "checkPopup", "minScore": (!isNaN(result2.minScore)) ? result2.minScore : 0.75});
                }
            }, error => {
                console.log("Error: " + error);
            });
        }, error => {
            console.log("Error: " + error);
        });
    }
});
