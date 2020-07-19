/*! Copyright (c) 
 * Sushil Gurung
 * Free licensed
 * Version: 1.0.0
 *
 */
(function ($) {
	Array.prototype.exists = function (x) {
		for (var i = 0; i < this.length; i++) {
			if (this[i] == x) return true;
		}
		return false;
	}
	$.fn.AllowNumberOnly = function (options) {
		var elem = $(this);
		var defaults = $.extend({
			isAllowDecimal: false,
			isAllowHypen: false
		}, options);
		var keyCodeNotAllow = [8, 9, 37, 39, 46];
		var keyAllow = [1, 2, 3, 4, 5, 6, 7, 8, 9, 0];
		var decimalNumber = 110;
		var decimalNo = 190;
		var hypenNumber = 189;
		var ctrlKey = [86, 67, 65, 88];
		if (defaults.isAllowDecimal) {
			keyCodeNotAllow.push(decimalNumber);
			keyCodeNotAllow.push(190);
			keyAllow.push(".");
		}
		if (defaults.isAllowHypen) {
			keyCodeNotAllow.push(hypenNumber);
		}
		this.each(function () {
			$(elem).off();
			$(elem).keydown(function (event) {
				//$(document).off('keydown', elem);
				//$(document).on('keydown', elem, function (event) {
				//alert(event.keyCode);
				if (event.shiftKey) {
					event.preventDefault();
				}
				if ((event.ctrlKey && ctrlKey.includes(event.keyCode)) || (event.ctrlKey && event.keyCode == 67) || (event.ctrlKey && event.keyCode == 65) || (event.keyCode >= 48 && event.keyCode <= 57) || (event.keyCode >= 96 && event.keyCode <= 105) || keyCodeNotAllow.includes(event.keyCode)) {
				
				} else {
					event.preventDefault();
				}
			}).on('paste', function (e) {
				var $this = $(this);
				setTimeout(function () {
					var value = $this.val();
					for (var i = 0; i < value.length; i++) {
						var key = value[i];
						if (keyAllow.exists(key)) {

						} else {
							$(elem).val("");
							jAlert("Positive Number Only", "alert");
							event.preventDefault();
						}
					}
				}, 4);
			});
		});
		return this;
	};
}(jQuery));