var GetDateTimePickerFormat = 'm/d/Y';
function reverseDateFormat(inputdate) {
	var dateformatArry = GetDateTimePickerFormat.split('/');
	var dayindex = dateformatArry.indexOf('d');
	var monthindex = dateformatArry.indexOf('m');
	var yearindex = dateformatArry.indexOf('Y');

	if (inputdate != null && inputdate != '') {
		var dateAr = inputdate.split('/');
		var newDate = dateAr[monthindex] + '/' + dateAr[dayindex] + '/' + dateAr[yearindex];
		return newDate;
	}
	else {
		return inputdate;
	}
}

//from serilaize to object
function formserializeToJson(form) {
	debugger
	// Find disabled inputs, and remove the "disabled" attribute
	//var disabled = form.find(':input:disabled').removeAttr('disabled');
	var formData = JSON.parse(JSON.stringify($(form).serializeArray()));
	var obj = {};
	$.each(formData, function (key, value) {
		obj[value.name] = value.value;
	});
	// re-disabled the set of inputs that you previously enabled
	//disabled.attr('disabled', 'disabled');
	return obj;
}


function setform(objData) {
	// var formData = JSON.parse(JSON.stringify($('#banform').serializeArray()));
	for (x in objData) {
		if ($('input[name="' + x + '"]').isElementExists()) {
			$('input[name="' + x + '"]').val(objData[x]);
		} else {
			console.log(x);
		}
	}
}
jQuery.fn.isElementExists = function () {
	return this.length;
}



jQuery.fn.serializeAllArray = function () {
	var array = [];
	var obj = {};
	$('input', this).each(function () {
		obj[this.name] = $(this).val();
	});
	return array.push(obj);
}



//});