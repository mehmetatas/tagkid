
$(function () {
    var findElementTotalOffset = function (obj) {
        var oleft = 0;
        var otop = 0;
        if (obj.offsetParent) {
            do {
                oleft += obj.offsetLeft;
                otop += obj.offsetTop;
            } while (obj = obj.offsetParent);
        }
        return { left: oleft, top: otop };
    };

    jQuery.fn.scrollTo = function (elem, speed, callback) {
        var elemOffset = findElementTotalOffset($(elem)[0]);

        $(this).animate({
            scrollTop: elemOffset.top - $(this).offset().top
        }, speed == undefined ? 1000 : speed, callback);
        return this;
    };

    $.fn.getCaretPosition = function () {
        var input = this.get(0);

        if (!input)
            return -1;

        if ('selectionStart' in input) {
            return input.selectionStart;
        } else if (document.selection) {
            input.focus();
            var sel = document.selection.createRange();
            var selLen = document.selection.createRange().text.length;
            sel.moveStart('character', -input.value.length);
            return sel.text.length - selLen;
        }
        return -1;
    };

    $.fn.setCaretPosition = function (caretPos) {
        var input = this.get(0);

        if (!input)
            return;

        if (input.createTextRange) {
            var range = input.createTextRange();
            range.move('character', caretPos);
            range.select();
        } else {
            if (input.selectionStart) {
                input.focus();
                input.setSelectionRange(caretPos, caretPos);
            } else {
                input.focus();
            }
        }
    };
});