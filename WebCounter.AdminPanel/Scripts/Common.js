function PopUpShowing(sender, eventArgs) {
    var popUp = eventArgs.get_popUp();
    $(popUp).css("position", "fixed");
    popUp.style.left = Math.round((($(document).width() - $(popUp).width()) / 2)).toString() + "px";
    popUp.style.top = Math.round($(popUp).height() / 2).toString() + "px";
}

function PopUpShowingTop(sender, eventArgs) {
    var popUp = eventArgs.get_popUp();
    $(popUp).css("position", "fixed");
    popUp.style.left = Math.round((($(document).width() - $(popUp).width()) / 2)).toString() + "px";
    popUp.style.top = "50px";
}