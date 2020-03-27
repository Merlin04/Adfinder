browser.tabs.onUpdated.addListener((tabId, changeInfo, tab) => {
    if(tab.status == "complete" && tab.active) {
        let getting = browser.storage.sync.get("enabled");
        getting.then(result => {
            let minScore = browser.storage.sync.get("minScore");
            minScore.then(result2 => {
                if ((result.enabled != undefined) ? result.enabled : true) {
                    browser.tabs.sendMessage(tabId, {"message": "checkPopup", "minScore": result2.minScore});
                }
            });
        }, error => {
            console.log("Error: " + error);
        });
    }
});