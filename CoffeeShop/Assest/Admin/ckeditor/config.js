/*
Copyright (c) 2003-2011, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function( config )
{
	// Define changes to default configuration here. For example:
    config.syntaxhighlight_lang = 'csharp';
    config.syntaxhighlight_hideControls = true;
	config.language = 'vi';
    config.filebrowserBrowseUrl = '/Assest/Admin/ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl = '/Assest/Admin/ckfinder/ckfinder.html?Type=Images';
    config.filebrowserFlashBrowseUrl = '/Assest/Admin/ckfinder/ckfinder.html?Type=Flash';
    config.filebrowserUploadUrl = '/Assest/Admin/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    config.filebrowserImageUploadUrl = '/Images';
    config.filebrowserFlashUploadUrl = '/Assest/Admin/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';
    config.autoParagraph = false;
    CKFinder.setupCKEditor(null, '/Assest/Admin/ckfinder/');
};
