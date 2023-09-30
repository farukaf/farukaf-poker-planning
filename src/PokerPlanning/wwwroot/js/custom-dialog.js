 
function dialogShow(trigger) {
    if (trigger) {
        document
            .querySelector(`[data-id="${trigger}"]`)
            .showModal();
    }
}