function DeleteButtonClick(urlToSend) {
    var c = confirm('Do you want to delete selected records?');
    if (!c) return false;
    var tb = jQuery(this).attr('title'); 						// get target id of table								   
    var sel = false; 											//initialize to false as no selected row
    var ch = jQuery(couponsUsable).find('tbody input[type=checkbox]');  	//get each checkbox in a table

    //check if there is/are selected row in table
    var dataArr;
    ch.each(function () {
        if (jQuery(this).attr('checked') == 'checked') {
            sel = true; 											//set to true if there is/are selected row
            console.log(jQuery(this).attr('checked'));
            //jQuery(this).parents('tr').fadeOut(function () {
            //jQuery(this).remove(); 							//remove row when animation is finished
            var anSelected = fnGetSelected(couponsUsable);
            if (anSelected != undefined && anSelected.length !== 0) {
                // couponsUsable.fnDeleteRow(anSelected[0]);
                dataArr = anSelected;
            }
            //});
        }
    });

    if (!sel) alert('No data selected'); 							//alert to no data selected
    else {
        DeleteMultipleRowsFromDB(dataArr,urlToSend);
    }
}

function eventFired(type) {
    console.log(type);
}

function PrepareDelete(link) {
    jQuery(link).parents('tr').addClass('selected');
    var ch = jQuery(link).parents('tr').find('td input[type=checkbox]');
    jQuery(ch).attr('checked', true);
    jQuery(ch).parent().addClass('checked');
    jQuery('.deletebutton').click();
}

function MakeTablePageable(objTable) {
    objTable.dataTable({
        "sPaginationType": "full_numbers",
        "aaSortingFixed": [[0, 'asc']],
        "fnDrawCallback": function (oSettings) {
            jQuery('input:checkbox,input:radio').uniform();
            //jQuery.uniform.update();
        }
    });
}

function fnGetSelected(oTableLocal) {
    return oTableLocal.find('tr.selected');
}

(function ($) {
    /*
    * Function: fnGetColumnData
    * Purpose:  Return an array of table values from a particular column.
    * Returns:  array string: 1d data array 
    * Inputs:   object:oSettings - dataTable settings object. This is always the last argument past to the function
    *           int:iColumn - the id of the column to extract the data from
    *           bool:bUnique - optional - if set to false duplicated values are not filtered out
    *           bool:bFiltered - optional - if set to false all the table data is used (not only the filtered)
    *           bool:bIgnoreEmpty - optional - if set to false empty values are not filtered from the result array
    * Author:   Benedikt Forchhammer <b.forchhammer /AT\ mind2.de>
    */
    $.fn.dataTableExt.oApi.fnGetColumnData = function (oSettings, iColumn, bUnique, bFiltered, bIgnoreEmpty) {
        // check that we have a column id
        if (typeof iColumn == "undefined") return new Array();

        // by default we only wany unique data
        if (typeof bUnique == "undefined") bUnique = true;

        // by default we do want to only look at filtered data
        if (typeof bFiltered == "undefined") bFiltered = true;

        // by default we do not wany to include empty values
        if (typeof bIgnoreEmpty == "undefined") bIgnoreEmpty = true;

        // list of rows which we're going to loop through
        var aiRows;

        // use only filtered rows
        if (bFiltered == true) aiRows = oSettings.aiDisplay;
        // use all rows
        else aiRows = oSettings.aiDisplayMaster; // all row numbers

        // set up data array    
        var asResultData = new Array();

        for (var i = 0, c = aiRows.length; i < c; i++) {
            iRow = aiRows[i];
            var aData = this.fnGetData(iRow);
            var sValue = aData[iColumn];
            if(sValue==undefined)continue;
            // ignore empty values?
            if (bIgnoreEmpty == true && sValue.length == 0) continue;

            // ignore unique values?
            else if (bUnique == true && jQuery.inArray(sValue, asResultData) > -1) continue;

            // else push the value onto the result data array
            else asResultData.push(sValue);
        }

        return asResultData;
    }
} (jQuery));

function fnCreateSelect(aData) {
    var r = '<select><option value=""></option>', i, iLen = aData.length;
    for (i = 0; i < iLen; i++) {
        r += '<option value="' + aData[i] + '">' + aData[i] + '</option>';
    }
    return r + '</select>';
}

function ArrangeCloseNotiBar() {
    ///// NOTIFICATION CLOSE BUTTON /////
    jQuery('.notibar .close').click(function () {
        jQuery(this).parent().fadeOut(function () {
            jQuery(this).remove();
        });
    });
}

function DeleteMultipleRowsFromDB(dataArr,urlToSend) {
            var arrToSend = [];
            jQuery.each(dataArr, function (i) {
                var tds = jQuery(dataArr[i]).find('td');
               // console.log("id:" + tds[1].innerText);
                arrToSend.push(tds[1].innerText);
            });
            jQuery.ajax({
                type: "POST",
                url:urlToSend,// "/Coupon/DeleteMultiple",
                dataType: "json",
                traditional: true,
                data: {
                    array: arrToSend
                },
                success: function (data) {
                    jQuery("#DescriptionDiv").append(data.Message);
                    ArrangeCloseNotiBar();
                    if (data.valid) {
                        DeleteRows(dataArr);
                    }
                }
            });
        }

        function DeleteRows(dataArr) {
            jQuery.each(dataArr, function (i) {
                couponsUsable.fnDeleteRow(dataArr[i]);
            });
        }


        