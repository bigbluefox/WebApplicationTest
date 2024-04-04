using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.SessionState;
using Hsp.Test.Common;

namespace WebApplicationTest.DropZone
{
    /// <summary>
    /// uploadHandler 的摘要说明
    /// </summary>
    public class uploadHandler : IHttpHandler, IRequiresSessionState
    {
        /// <summary>
        ///     附件虚拟目录
        /// </summary>
        internal string VirtualDirectory = ConfigurationManager.AppSettings["VirtualDirectory"].Trim();

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            context.Response.AddHeader("Cache-Control", "no-cache, must-revalidate");
            context.Response.AddHeader("Expires", "Mon, 26 Jul 2997 05:00:00 GMT");
            //context.Response.AddHeader("", "");

            context.Response.ContentType = "application/json";
            context.Response.Cache.SetNoStore();

            //is_drop_zone=true
            //&X-Requested-With=XMLHttpRequest
            //&X-File-Name=%E4%B8%AD%E5%9B%BD%E4%BA%BA%E6%B0%91%E8%A7%A3%E6%94%BE%E5%86%9B%E5%86%9B%E6%9C%8D%E5%8F%91%E5%B1%95%E5%8F%B2.docx
            //&X-File-Size=16277778&X-File-Id=0&X-File-Resume=false&j67gsgup

            //is_drop_zone = true
            //X-Requested-With = XMLHttpRequest
            //X-File-Name = 中国人民解放军军服发展史.docx
            //X-File-Size = 16277778
            //X-File-Id = 1
            //X-File-Resume = false

            try
            {
                var strParams = context.Request.Params["Params"];
                var filename = context.Request.Form["filename"];

                #region 文件保存

                var file = context.Request.Files["fileData"];

                if (file != null)
                {
                    var fileLength = file.ContentLength; // 附件长度
                    var contentType = file.ContentType; // 互联网媒体类型，Internet Media Type，MIME类型
                    filename = string.IsNullOrEmpty(filename) ? file.FileName : filename; // 附件名称 D:\新建文件夹 (4)\99A坦克.jpg

                    if (filename.LastIndexOf("\\", StringComparison.Ordinal) > -1)
                    {
                        filename = filename.Substring(filename.LastIndexOf("\\", StringComparison.Ordinal) + 1);
                    }

                    VirtualDirectory = VirtualDirectory.StartsWith("\\")
                        ? VirtualDirectory
                        : "\\" + VirtualDirectory;
                    VirtualDirectory = VirtualDirectory.EndsWith("\\")
                        ? VirtualDirectory
                        : VirtualDirectory + "\\";

                    var strSubdirectories = DateTime.Now.ToString("yyyy-MM-dd").Replace("-", "\\") + "\\"; // 以日期为附件子目录

                    strSubdirectories = strSubdirectories.EndsWith("\\")
                        ? strSubdirectories
                        : strSubdirectories + "\\";

                    // 检查附件目录
                    FileHelper.FilePathCheck(HttpContext.Current.Server.MapPath(VirtualDirectory + strSubdirectories));

                    var filePath = context.Server.MapPath(VirtualDirectory + strSubdirectories + filename);

                    file.SaveAs(filePath);
                }

                #endregion


                #region 测试传递变量

                var keyValue = "";
                var files = "";

                var s = "";
                foreach (var name in context.Request.Form)
                {
                    s += name + " = " + context.Request.Form[name.ToString()] + Environment.NewLine;
                }

                var a = s;
                s = "";

                foreach (var name in context.Request.QueryString)
                {
                    if (name != null)
                    {
                        s += name + " = " + context.Request.QueryString[name.ToString()] + Environment.NewLine;
                    }
                }

                var b = s;
                s = "";

                try
                {
                    foreach (var name in context.Request.Params)
                    {
                        if (name != null)
                        {
                            s += name + " = " + context.Request.Params[name.ToString()] + Environment.NewLine;
                        }
                    }
                }
                catch (Exception ex)
                {
                    //var exx = ex;
                    throw;
                }

                var c = s;
                s = "";

                foreach (var name in context.Request.Files)
                {
                    s += name + " = " + context.Request.Files[name.ToString()] + Environment.NewLine;
                }

                files = s;

                var aa = files;

                #endregion

                //{ \"imageUrl\": \"http://my.image/file.jpg\" }
                context.Response.Write("{ \"imageUrl\": \"http://my.image/file.jpg\" }"); 

            }
            catch (Exception ex)
            {
               context.Response.Write("{ \"error\": \"File could not be saved.\" }"); 
                //throw;
            }

            //header('Cache-Control: no-cache, must-revalidate');
            //header('Expires: Mon, 26 Jul 1997 05:00:00 GMT');
            //header('Content-type: application/json');
            //echo json_encode($response);

            // Load mooupload class
            //require('..'.DIRECTORY_SEPARATOR.'Source'.DIRECTORY_SEPARATOR.'mooupload.php');

            // Upload file to tmp directory
            //Mooupload::upload(dirname(__FILE__).DIRECTORY_SEPARATOR.'tmp'.DIRECTORY_SEPARATOR);
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}


//<?php

/**
 *
 * Mooupload class
 * 
 * Provides a easy way for recept and save files from MooUpload
 * 
 * DISCLAIMER: You must add your own special rules for limit the upload of
 * insecure files like .php, .asp or .htaccess
 *
 * DropZone DISCLAIMER: DropZone is a front-end project. This PHP class is provided with DropZone just for demo purposes, you gotta modify it to work with your website.
 * 
 * @author: Juan Lago <juanparati[at]gmail[dot].com>
 * 
 */

//class Mooupload 
//{

// Container index for HTML4 and Flash method
//const container_index = '_tbxFile';

/**
	 *
	 * Detect if upload method is HTML5
	 * 	 
	 * @return	boolean
	 * 	 	  	 
	 */

//public static function is_HTML5_upload()
//{
//    return empty($_FILES);
//}

/**
	 *
	 * Upload a file using HTML4 or Flash method
	 * 
	 * @param		string	Directory destination path 	  
	 * @param		string	File prefix (Useful for avoid file overwriting)	 	 
	 * @param		boolean	Return response to the script	 
	 * @return	array		Response
	 * 	 	  	 
	 */

//public static function HTML4_upload($destpath, $file_prefix = '', $send_response = TRUE)

//{


//    // Normalize path

//    $destpath = self::_normalize_path($destpath);


//    // Check if path exist

//    if (!file_exists($destpath))

//        throw new Exception('Path do not exist!');


//    // Upload file using traditional method	

//    $response = array();


//  foreach ($_FILES as $k => $file)

//  {

//    $response['key']         = (int)substr($k, strpos($k, self::container_index) + strlen(self::container_index));

//    $response['name']        = basename($file['name']);	// Basename for security issues

//    $response['error']       = $file['error'];

//    $response['size']        = $file['size'];

//    $response['upload_name'] = $file['name'];

//    $response['finish']      = FALSE;


//    if ($response['error'] == 0)

//    {

//      if (move_uploaded_file($file['tmp_name'], $destpath.$file_prefix.$file['name']) === FALSE)      

//        $response['error'] = UPLOAD_ERR_NO_TMP_DIR;

//      else

//        $response['finish'] = TRUE;


//    }          

//  }   


//    // Send response to iframe 

//    if ($send_response) 

//    echo json_encode($response);    				


//    return $response;	

//}


/**

	 *

	 * Upload a file using HTML5

	 * 

	 * @param		string	Directory destination path	 	 	 	 	 

	 * @param		boolean	Return response to the script	 

	 * @return	array		Response

	 * 	 	  	 

	 */

//public static function HTML5_upload($destpath, $file_prefix = '', $send_response = TRUE)

//{


//    // Normalize path

//    $destpath = self::_normalize_path($destpath);


//    // Check if path exist

//    if (!file_exists($destpath))

//        throw new Exception('Path do not exist!');


//  $max_upload 	= self::_convert_size(ini_get('upload_max_filesize'));

//  $max_post 		= self::_convert_size(ini_get('post_max_size'));

//  $memory_limit = self::_convert_size(ini_get('memory_limit'));


//  $limit = min($max_upload, $max_post, $memory_limit);


//  // Read headers

//  $response = array();

//    $headers 	= self::_read_headers();


//  $response['id']    	= $_GET['X-File-Id'];

//  $response['name']  	= basename($_GET['X-File-Name']); 	// Basename for security issues

//  $response['size']  	= $_GET['Content-Length'];

//  $response['error'] 	= UPLOAD_ERR_OK; 

//  $response['finish'] = FALSE;


//  // Detect upload errors

//    if ($response['size'] > $limit) 

//    $response['error'] = UPLOAD_ERR_INI_SIZE;


//    // Firefox 4 sometimes sends a empty packet as last packet

//    /*	       

//  else if ($headers['Content-Length'] == 0)

//    $response['error'] = UPLOAD_ERR_NO_FILE;

//  */	    	  


//  // Is resume?	  

//    $flag = (bool) $_GET['X-File-Resume'] ? FILE_APPEND : 0;


//  $filename = $response['id'].'_'.$response['name'];


//  $response['upload_name'] = $filename;


//  // Write file

//    if (file_put_contents($destpath.$filename, file_get_contents('php://input'), $flag) === FALSE)

//    $response['error'] = UPLOAD_ERR_CANT_WRITE;

//  else

//  {

//    if (filesize($destpath.$filename) == $headers['X-File-Size'])

//    {

//      $response['finish'] = TRUE;


//      /* If uploaded file is finished, maybe you are interested in saving, registering or moving the file */

//            // my_save_file($destpath.$filename, $file_prefix.$response['name']);

//    }

//  } 


//    // Return an ajax response

//    if ($send_response)

//    {

//        header('Cache-Control: no-cache, must-revalidate');

//      header('Expires: Mon, 26 Jul 1997 05:00:00 GMT');

//      header('Content-type: application/json');

//      echo json_encode($response);

//    }


//  return $response;

//}


/**

	 *	

	 * Detect the upload method and process the files uploaded

	 * 

	 * @param		string	Directory destination path	 	 	 	 	 

	 * @param		boolean	Return response to the script	 

	 * @return	array		Response

	 * 

	 */

//public static function upload($destpath, $file_prefix = '', $send_response = TRUE)

//{			

//    return self::is_HTML5_upload() ? self::HTML5_upload($destpath, $file_prefix, $send_response) : self::HTML4_upload($destpath, $file_prefix, $send_response);		

//}	 		 	 	


//    /**

//     *

//     * Convert to bytes a information scale

//     * 

//     * @param		string	Information scale

//     * @return	integer	Size in bytes	 

//     *

//     */	 	 	 	 

//    public static function _convert_size($val)

//    {

//        $val = trim($val);

//      $last = strtolower($val[strlen($val) - 1]);


//      switch ($last) {

//        case 'g': $val *= 1024;


//        case 'm': $val *= 1024;


//        case 'k': $val *= 1024;

//      }


//      return $val;

//    }	


//    /**

//     *

//     * Normalize a directory path

//     * 

//     * @param		string	Directory path

//     * @return	string	Path normalized	 

//     *

//     */	 	 	 	 

//    public static function _normalize_path($path)

//    {

//        if ($path[sizeof($path) - 1] != DIRECTORY_SEPARATOR)

//            $path .= DIRECTORY_SEPARATOR;


//        return $path; 

//    }	


//    /**

//     *

//     * Read and normalize headers

//     * 	 

//     * @return	array	 

//     *

//     */

//    public static function _read_headers()

//    {


//        // GetAllHeaders doesn't work with PHP-CGI

//        if (function_exists('getallheaders')) 

//        {

//            $headers = getallheaders();

//        }

//        else 

//        {

//            $headers = array();

//            $headers['Content-Length'] 	= $_SERVER['CONTENT_LENGTH'];

//            $headers['X-File-Id'] 		= $_SERVER['HTTP_X_FILE_ID'];

//            $headers['X-File-Name'] 	= $_SERVER['HTTP_X_FILE_NAME'];			

//            $headers['X-File-Resume'] 	= $_SERVER['HTTP_X_FILE_RESUME'];

//            $headers['X-File-Size'] 	= $_SERVER['HTTP_X_FILE_SIZE'];

//        }


//        return $headers;


//    }	 	 	


//}


//?>