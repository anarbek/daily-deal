

function SubmitData(target, submitBt, data) {

    if (!target || target == '') {
        // Page url without hash
        target = document.location.href.match(/^([^#]+)/)[1];
    }
    jQuery.ajax({
        url: target,
        dataType: 'json',
        type: 'POST',
        data: data,
        success: function (data, textStatus, XMLHttpRequest) {
            if (data.valid) {
                // alert(data.Message);
                jQuery("#DescriptionDiv").show();
                jQuery("#DescriptionDiv").html(data.Message);
                submitBt.removeAttr("disabled");
            }
            else {
                // Message
                //alert("not valid data");
                jQuery("#DescriptionDiv").html(data.Message);
                submitBt.removeAttr("disabled");
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            // Message
            alert("error DB : " + data.Message);
            submitBt.removeAttr("disabled");
        }
    });
}

function ajaxValidate(formToSubmit, submitBt) {
    var result = formToSubmit.validate().form();
    if (result) {
        submitBt.attr("disabled", "disabled");
    }
    return result;
}

function Delete(urlData, idVal) {
    if (confirm('Do you want to delete?')) {
        var submitBt = jQuery("#btnSubmit");
        submitBt.attr("disabled", "disabled");
        var sendTimer = new Date().getTime();
        jQuery.ajax({
            url: urlData,
            dataType: 'json',
            type: 'POST',
            data: { id: idVal },
            success: function (data, textStatus, XMLHttpRequest) {
                if (data.valid) {
                    // alert(data.Message);
                    jQuery("#DescriptionDiv").show();
                    jQuery("#DescriptionDiv").html(data.Message);
                    submitBt.removeAttr("disabled");

                    RedirectIfPossible(sendTimer, data);

                }
                else {
                    // Message
                    alert("not valid data");
                    submitBt.removeAttr("disabled");
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                // Message
                alert("error DB : " + data.Message);
                submitBt.removeAttr("disabled");
            }
        });
    }
} 


function RedirectIfPossible(sendTimer, data) {
    if (data.redirect != "none") {
        var receiveTimer = new Date().getTime();
        if (receiveTimer - sendTimer < 500) {
            setTimeout(function () {
                document.location.href = data.redirect;
            }, 500 - (receiveTimer - sendTimer));
        }
        else {
            document.location.href = data.redirect;
        }
    }
}

function MakeDatePicker(myObject) {
    myObject.datepicker({ changeMonth: true, changeYear: true,
        closeText: 'kapat',
        prevText: '«',
        nextText: '»',
        currentText: 'bugün',
        monthNames: ['Ocak', 'Şubat', 'Mart', 'Nisan', 'Mayıs', 'Haziran',
		'Temmuz', 'Ağustos', 'Eylül', 'Ekim', 'Kasım', 'Aralık'],
        monthNamesShort: ['Oca', 'Şub', 'Mar', 'Nis', 'May', 'Haz',
		'Tem', 'Ağu', 'Eyl', 'Eki', 'Kas', 'Ara'],
        dayNames: ['Pazar', 'Pazartesi', 'Salı', 'Çarşamba', 'Perşembe', 'Cuma', 'Cumartesi'],
        dayNamesShort: ['Pz', 'Pt', 'Sa', 'Ça', 'Pe', 'Cu', 'Ct'],
        dayNamesMin: ['Pz', 'Pt', 'Sa', 'Ça', 'Pe', 'Cu', 'Ct'],
        dateFormat: 'dd.mm.yy', firstDay: 1,
        isRTL: false
    });
    myObject.attr("readonly", "");
    myObject.attr("style", "width: 110px;");
}