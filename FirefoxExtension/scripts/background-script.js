browser.tabs.onUpdated.addListener((tabId, changeInfo, tab) => {
    if(tab.status == "complete" && tab.active) {
        browser.tabs.sendMessage(tabId, {"message": "checkPopup"});
    }
})