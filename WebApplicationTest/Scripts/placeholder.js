/*
 * jQuery placeholder, fix for IE6,7,8,9
 * @website itmyhome.com
 */
var JPlaceHolder = {
    //检测
    _check : function(){
        return 'placeholder' in document.createElement('input');
    },
    //初始化
    init : function(){
        if(!this._check()){
            this.fix();
        }
    },
    //修复
    fix : function(){
        jQuery(':input[placeholder]').each(function(index, element) {

            var fontSize = 12, paddingTop = 0, paddingLeft = 2;
            if (typeof $.fn.popover == 'function') {
                // 检查页面上是否加载了bootstrap
                fontSize = 14, paddingTop = 7, paddingLeft = 10;
            }

            var self = $(this), txt = self.attr('placeholder');
            self.wrap($('<div></div>').css({position:'relative', zoom:'1', border:'none', background:'none', padding:'none', margin:'none'}));
            var pos = self.position(), h = self.outerHeight(true), paddingleft = self.css('padding-left');
           
            var holder = $('<span style="font-size: ' + fontSize + 'px;padding-top: ' + paddingTop + 'px"></span>').text(txt).css({ position: 'absolute', left: pos.left, top: pos.top, height: h, lienHeight: h, paddingLeft: paddingLeft + 'px', color: '#aaa' }).appendTo(self.parent());
            self.focusin(function(e) {
                holder.hide();
            }).focusout(function(e) {
                if(!self.val()){
                    holder.show();
                }
            });
            holder.click(function(e) {
                holder.hide();
                self.focus();
            });
        });
    }
};

//执行
jQuery(function(){
    JPlaceHolder.init();    
});