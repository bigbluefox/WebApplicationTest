/**
 * @license Copyright (c) 2003-2019, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
    // config.uiColor = '#AADC6E';

    // ckeditor:�������ݵ�ckeditorʱ��ֻ�����ı�����������ʽ������� 
    //config.forcePasteAsPlainText = true; 

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


    config.image_previewText = " "; //Ԥ��������ʾ����

    // ��2��ʵ�� asp.net mvc ckeditor ͼƬ�ϴ� 
    //config.filebrowserImageUploadUrl = "/SystemMgmt/Attachment/Upload/";
    config.filebrowserImageUploadUrl = "/Handler/CkeditorImageHandler.ashx";

    // �����°�ckeditor�������Ʒ��������ϴ���Դ�õģ�����Ĭ���ǿ����ģ�����ֻ��Ҫ�ر���Ӧ�Ĳ�����ɽ�����쳣
    config.removePlugins = 'easyimage,cloudservices';//��Ӹ��д���ر�easyimage��cloudservices�������



};
