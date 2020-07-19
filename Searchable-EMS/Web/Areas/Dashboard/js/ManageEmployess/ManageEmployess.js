(function ($) {
	const Toast = Swal.mixin({
		toast: true,
		position: 'top-end',
		showConfirmButton: false,
		timer: 3000
	});
	$.ManageEmployessExtend = function (p) {
		p = $.extend({
			productId: 0,
			//Disable: true,
			//English: true
		}, p);
		var EmployeeManage = {
			config: {
				isPostBack: false,
				async: false,
				cache: false,
				type: 'POST',
				contentType: "application/json; charset=utf-8",
				data: '{}',
				dataType: 'json',
				crossDomain: true,
				baseURL: rootUrl + 'api/ManageEmployess/',
				method: "",
				url: "",
				ajaxCallMode: '',
				offset: 1,
				viewPerPage: 10,
				GetDateTimePickerFormat: 'm/d/Y',
				EmployeId: 0,
				ImageName: '',
				headers: { Authorization: 'Bearer ' + token }
			},
			ajaxCall: function (config) {
				$.ajax({
					type: EmployeeManage.config.type,
					headers: EmployeeManage.config.headers,
					contentType: EmployeeManage.config.contentType,
					async: EmployeeManage.config.async,
					cache: EmployeeManage.config.cache,
					url: EmployeeManage.config.url,
					data: EmployeeManage.config.data,
					dataType: EmployeeManage.config.dataType,
					success: EmployeeManage.config.ajaxCallMode,
					error: EmployeeManage.ajaxFailure
				});
			},
			ajaxFileUpload: function (config) {
				$.ajax({
					url: EmployeeManage.config.url,
					headers: EmployeeManage.config.headers,
					processData: false,
					contentType: false,
					data: EmployeeManage.config.data,
					type: EmployeeManage.config.type,
					success: EmployeeManage.config.ajaxCallMode,
					error: EmployeeManage.ajaxFailure
				});
			},
			ajaxFileExport: function (config) {
				$.ajax({
					url: EmployeeManage.config.url,
					headers: EmployeeManage.config.headers,
					processData: false,
					contentType: false,
					xhrFields: { responseType: 'blob' },
					//data: EmployeeManage.config.data,
					type: EmployeeManage.config.type,
					success: EmployeeManage.config.ajaxCallMode,
					error: EmployeeManage.ajaxFailure
				});
			},
			Init: function () {
				EmployeeManage.InitPlugins();
				EmployeeManage.EventHandler();
				EmployeeManage.GetEmployeeList(EmployeeManage.config.offset, EmployeeManage.config.viewPerPage, 0);
			},
			InitPlugins() {
				$("#txtSalaryRangeFrom,#txtSalaryRangeTo").AllowNumberOnly({
					isAllowDecimal: true
				});

				$("#txtFromDate").datetimepicker({
					format: EmployeeManage.config.GetDateTimePickerFormat,
					timepicker: false,
					changeMonth: true,
					changeYear: true,
					//minDate: 0,
					//maxDate: '+1970/01/02',
					onChangeDateTime: function (dp, $input) {
						$("#txtToDate").datetimepicker({ minDate: dp });
					},
				});

				$("#txtToDate").datetimepicker({
					format: EmployeeManage.config.GetDateTimePickerFormat,
					timepicker: false,
					maxDate: 0,
					onChangeDateTime: function (dp, $input) {
						//if (dp != null) {
						//	if ($("#txtFromDate").val() != '') {
						//		$("#txtFromDate").datetimepicker({ maxDate: dp });
						//	} else {
						//		alert("Please Select the From Date");
						//	}
						//}
					},
				});

				$("#txtDateOfBirth").datetimepicker({
					format: EmployeeManage.config.GetDateTimePickerFormat,
					timepicker: false,
					maxDate: 0,
					onChangeDateTime: function (dp, $input) {
					},
				});


				$("#txtFromImportDate").datetimepicker({
					format: EmployeeManage.config.GetDateTimePickerFormat,
					timepicker: false,
					changeMonth: true,
					changeYear: true,
					//minDate: 0,
					//maxDate: '+1970/01/02',
					onChangeDateTime: function (dp, $input) {
						//$("#txtToDate").datetimepicker({ minDate: dp });
					},
				});

				$("#txtToImportDate").datetimepicker({
					format: EmployeeManage.config.GetDateTimePickerFormat,
					timepicker: false,
					changeMonth: true,
					changeYear: true,
					//minDate: 0,
					//maxDate: '+1970/01/02',
					onChangeDateTime: function (dp, $input) {
						//$("#txtToDate").datetimepicker({ minDate: dp });
					},
				});




			},
			EventHandler: function () {
				EmployeeManage.SearchHandler();

				$('#file-uploads').on('change', function () {
					EmployeeManage.ImportFile();
				});


				$('#chk-selectAll').on('change', function () {
					$('input[type=checkbox][name=ChkEmployee]').not(this).prop('checked', this.checked);
				});

				$('#btnCreate').on('click', function () {
					EmployeeManage.ClearForm();
					$("#modal-add-update").modal();
				});
				$('#btnSubmit').on('click', function () {
					if (validator.form()) {
						EmployeeManage.SubmitForm();
					}
				});
				$('#btnCancel').on('click', function () {
					EmployeeManage.ClearForm();
				});

				$('#image-uploads').on('change', function () {
					EmployeeManage.UploadImage();
				});

				$('#tblEmployee').on('click', 'button[data-edit]', function () {
					let id = $(this).attr('data-edit');
					EmployeeManage.GetEmployeeDetails(id);
				});

				$('#btn-downloads-xls').on('click', function () {
					EmployeeManage.ExportFiles(1);
				});
				$('#btn-downloads-csv').on('click', function () {
					EmployeeManage.ExportFiles(2);
				});

				$('#btn-downloads-pdf').on('click', function () {
					EmployeeManage.ExportFiles(3);
				});

				$('#btnPrint').on('click', function () {
					$("html").printThis();
				});



			},
			SearchHandler: function () {
				$('#txtSearchText').on('keyup', function () {
					EmployeeManage.GetEmployeeList(EmployeeManage.config.offset, EmployeeManage.config.viewPerPage, 0);
				});

				$('#ddlGenderSearch').on('change', function () {
					EmployeeManage.GetEmployeeList(EmployeeManage.config.offset, EmployeeManage.config.viewPerPage, 0);
				});

				$('#txtSalaryRangeTo').on('change', function () {
					EmployeeManage.GetEmployeeList(EmployeeManage.config.offset, EmployeeManage.config.viewPerPage, 0);
				});

				$('#ddlPage').on('change', function () {
					EmployeeManage.config.viewPerPage = $(this).val();
					EmployeeManage.GetEmployeeList(EmployeeManage.config.offset, EmployeeManage.config.viewPerPage, 0);
				});
				$('#btn-Search').on('click', function () {
					EmployeeManage.GetEmployeeList(EmployeeManage.config.offset, EmployeeManage.config.viewPerPage, 0);
				});

			},
			ImportFile: function () {
				let data = new FormData();
				let file = $('#file-uploads')[0].files[0];
				data.append('file', file);
				EmployeeManage.config.data = data;
				EmployeeManage.config.data = data;
				EmployeeManage.config.type = "POST";
				EmployeeManage.config.url = EmployeeManage.config.baseURL + "UploadFile";
				EmployeeManage.config.ajaxCallMode = EmployeeManage.BindUploadFile;
				EmployeeManage.ajaxFileUpload(EmployeeManage.config);
			},
			BindUploadFile: function (data) {
				let response = data;
				if (response.Status) {
					if (response.Validation) {
						let html = '';
						$.each(response.Entity, function (index, item) {
							html += '<tr>';
							html += '<td>' + item.FullName + '</td>';
							html += '<td>' + item.DateOfBirth + '</td>';
							html += '<td>' + item.Gender + '</td>';
							html += '<td>' + item.Salary + '</td>';
							html += '<td>' + item.Designation + '</td>';
							html += '</tr>';
						});
						$('#sucess-List').html(html);
						if (response.SkipedRow.length) {
							$('#skip-List').html('X rows [' + response.SkipedRow + '] were skipped since they did not have data');
						}
						$("#modal-default").modal();
						EmployeeManage.GetEmployeeList(EmployeeManage.config.offset, EmployeeManage.config.viewPerPage, 0);
					} else {
						let error = '';
						$.each(response.ExcelValidationError, function (index, item) {
							error += '<tr>';
							error += '<td>' + item + '</td>';
							error += '</tr>';
						});
						$('#error-List').html(error);
						$("#modal-validation-error").modal();
					}
				} else {
					let error = '';
					error += '<tr>';
					error += '<td>' + response.ReturnMessage + '</td>';
					error += '</tr>';
					$('#error-List').html(error);
					$("#modal-validation-error").modal();
				}
			},
			GetEmployeeList: function (offset, limit, current) {
				let obj = {
					Offset: offset,
					Limit: limit,
					DateFrom: reverseDateFormat($.trim($("#txtFromDate").val())),
					DateTo: reverseDateFormat($.trim($("#txtToDate").val())),
					SearchText: $('#txtSearchText').val(),
					SalaryRangeFrom: $('#txtSalaryRangeFrom').val(),
					SalaryRangeTo: $('#txtSalaryRangeTo').val(),
					Gender: $('#ddlGenderSearch').val(),
					ImportedRangeFrom: reverseDateFormat($.trim($("#txtFromImportDate").val())),
					ImportedRangeTo: reverseDateFormat($.trim($("#txtToImportDate").val()))
				};
				EmployeeManage.config.type = "GET";
				EmployeeManage.config.currentPage = current;
				EmployeeManage.config.url = EmployeeManage.config.baseURL + "GetEmployeeList";
				EmployeeManage.config.data = obj
				EmployeeManage.config.ajaxCallMode = EmployeeManage.BindEmployeeList;
				EmployeeManage.ajaxCall(EmployeeManage.config);
			},
			BindEmployeeList: function (data) {
				let response = data;
				let html = '';
				if (parseInt(response.Entity.length) > 0) {
					$.each(response.Entity, function (index, item) {
						response.TotalRows = item.RowTotal;
						html += '<tr>';
						html += '<td><input type="checkbox" id="emp-' + item.Id + '" name="ChkEmployee" data-id="' + item.Id + '" value="' + item.Id + '"></td>';
						html += '<td>' + item.FullName + '</td>';
						html += '<td>' + item.DateOfBirthView + '</td>';
						html += '<td>' + item.Gender + '</td>';
						html += '<td>' + item.Salary + '</td>';
						html += '<td>' + item.Designation + '</td>';

						html += '<td>';
						html += EmployeeManage.BindAction(item);
						html += '</td>';
						html += '</tr>';
					});
				} else {
					html += '<tr>';
					html += '<td>No Records</td>';
					html += '</tr>';
				}
				$('#tblEmployee').html(html);
				if (parseInt(response.TotalRows) <= parseInt(EmployeeManage.config.viewPerPage)) {
					$('#pagination').hide();
				}
				else {
					$("#pagination").pagination(response.TotalRows,
						{
							items_per_page: EmployeeManage.config.viewPerPage,
							current_page: EmployeeManage.config.currentPage,
							callfunction: true,
							function_name: { name: EmployeeManage.GetEmployeeList, limit: EmployeeManage.config.viewPerPage },
							prev_text: "<",
							next_text: ">"
						});
					$('#pagination').show();
				}
				$('table').tablesorter({
					widgets: ['zebra', 'columns'],
					usNumberFormat: false,
					sortReset: true,
					sortRestart: true
				});

			},

			BindAction: function (item) {
				let html = '';
				html += '<div class="btn-group">';
				//html += '<button type="button" data-id="' + item.Id + '" class="btn btn-default btn-sm"><i class="fa fa-trash-o"></i></button>';
				html += '<button type="button" data-edit="' + item.Id + '" data-id="' + item.Id + '" class="btn btn-default btn-sm"><i class="fa fa-pencil-square-o"></i></button>';
				html += '</div>';
				//let fragment = $(html);
				//$('#tblEmployee button').on('click', function () {
				//	alert('');
				//});
				return html;
			},

			ClearForm: function () {
				$('#txtFullName').val('');
				$('#txtDateOfBirth').val('');
				$('#ddlGender').val(1);
				$('#txtSalary').val('');
				$('#txtDesgination').val('');
				$('#image-uploads').val('');
				validator.resetForm();
				EmployeeManage.config.EmployeId = 0;
				EmployeeManage.config.ImageName = '';
				$('#image-block').hide();
			},
			SubmitForm: function () {
				let obj = {
					Id: EmployeeManage.config.EmployeId,
					FullName: $.trim($('#txtFullName').val()),
					DateOfBirth: reverseDateFormat($.trim($("#txtDateOfBirth").val())),
					Gender: $.trim($('#ddlGender').val()),
					Salary: $.trim($('#txtSalary').val(1)),
					Designation: $.trim($('#txtDesgination').val()),
					ImageName: EmployeeManage.config.ImageName
				};
				EmployeeManage.config.type = "POST";
				EmployeeManage.config.url = EmployeeManage.config.baseURL + "AddUpdate";
				EmployeeManage.config.data = JSON.stringify(obj);
				EmployeeManage.config.ajaxCallMode = EmployeeManage.ShowStatus;
				EmployeeManage.ajaxCall(EmployeeManage.config);
			},
			ShowStatus: function (data) {
				let response = data;
				if (response.Status)
					Toast.fire({
						icon: 'success',
						title: response.ReturnMessage
					});
				EmployeeManage.GetEmployeeList(EmployeeManage.config.offset, EmployeeManage.config.viewPerPage, 0);
				$("#modal-add-update").modal('hide');
			},
			UploadImage: function () {
				let data = new FormData();
				let file = $('#image-uploads')[0].files[0];
				data.append('file', file);
				EmployeeManage.config.data = data;
				EmployeeManage.config.type = "POST";
				EmployeeManage.config.url = EmployeeManage.config.baseURL + "UploadImage";
				EmployeeManage.config.ajaxCallMode = EmployeeManage.BindImageUploads;
				EmployeeManage.ajaxFileUpload(EmployeeManage.config);
			},
			BindImageUploads: function (data) {
				let response = data
				if (response.Status) {
					$('#img-error-msg').html('').hide();
					EmployeeManage.config.ImageName = response.Entity;
					$('#image-Attachment').attr('src', rootUrl + '/File/Employee/Original/' + response.Entity);
					$('#image-block').show();
				} else {
					$('#img-error-msg').html(response.ReturnMessage).show();
				}
			},
			GetEmployeeDetails: function (id) {
				let obj = {
					id: id,
				};
				EmployeeManage.config.type = "GET";
				EmployeeManage.config.url = EmployeeManage.config.baseURL + "GetEmployeeDetails";
				EmployeeManage.config.data = obj
				EmployeeManage.config.ajaxCallMode = EmployeeManage.BindEmployeeDetails;
				EmployeeManage.ajaxCall(EmployeeManage.config);
			},
			BindEmployeeDetails: function (data) {
				let response = data.Entity;
				if (data.Status) {
					EmployeeManage.ClearForm();
					EmployeeManage.config.EmployeId = response.Id;
					$('#txtFullName').val(response.FullName);
					$('#txtDateOfBirth').val(response.DateOfBirthView);
					$('#ddlGender').val(response.Gender);
					$('#txtSalary').val(response.Salary);
					$('#txtDesgination').val(response.Designation);
					EmployeeManage.config.ImageName = response.ImageName;
					if (EmployeeManage.config.ImageName != null && EmployeeManage.config.ImageName != '') {
						$('#image-Attachment').attr('src', rootUrl + '/File/Employee/Original/' + EmployeeManage.config.ImageName);
						$('#image-block').show();
					} else {
						$('#image-block').hide();
					}

					$("#modal-add-update").modal();
				}
			},
			ExportFiles: function (filetype) {
				let ids = [];
				$("input:checkbox[name=ChkEmployee]:checked").each(function () {
					ids.push($(this).val());
				});
				if (ids.length > 0) {
					let exportIds = ids.join(',');



					//console.log(exportIds);
					let link = document.createElement("a");
					link.download = "names";
					//					link.href = EmployeeManage.config.baseURL + "DownloadFile?exportid=" + ids.join(',');
					link.href = rootUrl + "api/DashBoard/DownloadFile?exportid=" + exportIds + "&fileType=" + filetype;

					link.click();






				} else {
					Toast.fire({
						icon: 'error',
						title: 'Please select employee'
					});
				}


			}
		};
		var validator = $("#form-mange").validate({
			ignore: ":hidden",
			rules: {
				FullName: {
					required: true
				},
				DateOfBirth: {
					required: true,
				},
				Gender: {
					required: true
				},
				Salary: {
					required: true,
					number: true
				},
				Desgination: {
					required: true
				},
			},
			messages: {
				FullName: {
					required: ""
				},
				DateOfBirth: {
					required: "",
				},
				Gender: {
					required: ""
				},
				Salary: {
					required: "",
					number: "Number only"
				},
				Desgination: {
					required: "",
				},
			}
		});
		EmployeeManage.Init();
	};
	$.fn.ManageEmployess = function (p) {
		$.ManageEmployessExtend(p);
	};
})(jQuery);