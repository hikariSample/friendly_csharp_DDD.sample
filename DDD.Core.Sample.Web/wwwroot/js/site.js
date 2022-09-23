// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var TableInit = function () {
    var oTableInit = new Object();
    //初始化Table
    oTableInit.Init = function () {
        $('#table').bootstrapTable({
            //search: true,                       //搜索
            striped: true,                      //是否显示行间隔色
            pagination: true,                   //是否显示分页（*）
            showRefresh: true,
            showToggle: true,
            showColumns: true,
            iconSize: 'outline',
            toolbar: '#toolbar',
            sidePagination: "server",           //分页方式：client客户端分页，server服务端分页（*）
            queryParams: oTableInit.queryParams //传递参数（*）
        });
    };

    //得到查询的参数
    oTableInit.queryParams = function (params) {
        var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            limit: params.limit,   //页面大小
            offset: params.offset,  //页码
            // unitId: '@User.Claims.FirstOrDefault(claim => claim.Type == "UnitId").Value',
            // accountType:'@ViewBag.AccountType'
            //sdate: $("#stratTime").val(),
            //edate: $("#endTime").val(),
            //sellerid: $("#sellerid").val(),
            //orderid: $("#orderid").val(),
            //CardNumber: $("#CardNumber").val(),
            //maxrows: params.limit,
            //pageindex: params.pageNumber,

        };
        var where = $("#table").data("where");
        if (where != undefined) {
            var objWhere = eval('(' + where + ')');
            var whereKeys = Object.keys(objWhere);

            whereKeys.forEach(function (key) {
                temp[key] = objWhere[key];
            });
        }


        var form = $('form.ajaxTable');
        if (form.length > 0) {
            var t = form.serializeArray();
            $.each(t, function () {
                if (this.value != '') {
                    temp[this.name] = this.value;
                }

            });
        }
        return temp;
    };
    return oTableInit;
};
jQuery(document).ready(function ($) {
    //1.初始化Table
    var oTable = new TableInit();
    oTable.Init();


    $("body").on('click', "a.ajaxLink", function (e) {
        e.preventDefault();
        var t = $(this);
        var title = t.data("title");
        var url = t.data("href");
        var index = layer.open({
            type: 2,
            title: title,
            content: url
        });
        layer.full(index);
    });
    //表格搜索
    $("body").on('submit', "form.ajaxTable", function (e) {
        e.preventDefault();
        $('.table').bootstrapTable('refresh');
    });

    $("body").on('submit', "form.ajaxForm", function (e) {
        ajaxSubmit($(this), null);
        return false;
    });

    var ajaxSubmit = function (ajaxForm, conform) {

        parent.layx.load('loadId', '数据正在提交中，请稍后');
        ajaxForm.ajaxSubmit({
            data: {
                conform: conform,
            },
            success: function (msg) {
	            parent.layx.destroy('loadId');
                if (msg.success == true) {
                    var mg = msg.errorMsg;
                    if (mg == null) {
                        mg = "保存成功";
                    }
                    parent.layx.msg(mg, { dialogIcon: 'success' });

                    var form = parent.$('form.ajaxTable');
                    if (form.length > 0) {
	                    parent.$('#table').bootstrapTable('refresh');
                    } else {
                        if (parent.layx.getWindow('taobaosite') == undefined) {
                            window.location.reload();
                        } else {
                            parent.window.location.reload();
                        }
                    }
                    parent.layx.destroy('taobaosite');
                } else if (msg.success == false) {
                    var mg = msg.errorMsg;
                    if (mg == null) {
                        mg = "保存失败!";
                    }

                    parent.layx.msg(mg, { dialogIcon: 'error' });
                    console.error(mg);

                } else {
                    var index = parent.layer.getFrameIndex(window.name);
                    var body = index == undefined ? $('body') : parent.layer.getChildFrame('body', index);
                    for (var item in msg) {
                        body.find('[data-valmsg-for="' + item + '"]').attr('class', 'field-validation-error').html('<span id="' + item + '-error" class>' + msg[item] + '</span>');
                    }
                }
                
            },
            error: function (res) {
                layx.msg('服务器错误', { dialogIcon: 'error' });
                parent.layx.destroy('loadId');
                console.error(msg.errorMsg);
            }
        });
    }

});



/*弹出层*/
/*
	参数解释：
	title	标题
	url		请求的url
	id		需要操作的数据id
	w		弹出层宽度（缺省调默认值）
	h		弹出层高度（缺省调默认值）
*/
function layer_show(title, url, w, h) {
    if (title == null || title == '') {
        title = false;
    };
    if (url == null || url == '') {
        url = "404.html";
    };
    if (w == null || w == '') {
        w = 800;
    };
    if (h == null || h == '') {
        h = ($(window).height() - 50);
    };
    layx.iframe('taobaosite', title, url);
}
/*关闭弹出框口*/
function layer_close() {
    var index = parent.layer.getFrameIndex(window.name);
    parent.layer.close(index);
}
function layer_full(title, url) {
    var index = layer.open({
        type: 2,
        fix: false, //不固定
        shadeClose: true,
        shade: 0.4,
        maxmin: true, //开启最大化最小化按钮
        area: ['893px', '600px'],
        title: title,
        content: url
    });
    layer.full(index);
}