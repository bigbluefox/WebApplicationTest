/**
 * @license Copyright (c) 2003-2017, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function(config) {

    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    // config.uiColor = '#AADC6E';

    config.toolbarGroups = [
        { name: "document", groups: ["mode", "document", "doctools"] },
        { name: "clipboard", groups: ["clipboard", "undo"] },
        { name: "editing", groups: ["find", "selection", "editing"] },
        { name: "insert", groups: ["insert"] },
        { name: "links", groups: ["links"] },
        { name: "colors", groups: ["colors"] },
        { name: "tools", groups: ["tools"] },
        "/",
        { name: "basicstyles", groups: ["basicstyles", "cleanup"] },
        { name: "paragraph", groups: ["list", "indent", "blocks", "align", "bidi", "paragraph"] },
        { name: "styles", groups: ["styles"] },
        { name: "about", groups: ["about"] }
    ];

    config.removeButtons = "BidiLtr,BidiRtl,Language,Anchor,Flash,Smiley,Iframe,PageBreak,Form,Checkbox,Radio,TextField,Textarea,Select,Button,ImageButton,HiddenField,Save,Print,Scayt,Styles,NewPage,CreateDiv";

    config.extraPlugins = "uploadimage";

    // 复制内容到ckeditor时，只保留文本，忽略其样式解决方法 
    config.forcePasteAsPlainText = true;

    config.image_previewText = " "; //预览区域显示内容

    // 简单2步实现 asp.net mvc ckeditor 图片上传 
    config.filebrowserImageUploadUrl = "/Handler/CKEditorUploadHandler.ashx";
};