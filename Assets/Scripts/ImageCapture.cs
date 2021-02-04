using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.WSA.Input;


// take pictures, send them to api and process results

public class ImageCapture : MonoBehaviour
{
    /// Allows this class to behave like a singleton
    public static ImageCapture instance;

    /// Keeps track of tapCounts to name the captured images 
    private int tapsCount;

    /// PhotoCapture object used to capture images on HoloLens 
    private UnityEngine.Windows.WebCam.PhotoCapture photoCaptureObject = null;

    /// HoloLens class to capture user gestures
    private GestureRecognizer recognizer;

    /// Initialises this class
    private void Awake()
    {
        Debug.Log("ImageCapture Service Script is running AWAKE");
        instance = this;
    }

    /// Called right after Awake
    void Start()
    {

    }



    /// Begin process of Image Capturing and send To Azure Computer Vision service.
    private void ExecuteImageCaptureAndAnalysis()
    {
        Resolution cameraResolution = UnityEngine.Windows.WebCam.PhotoCapture.SupportedResolutions.OrderByDescending
            ((res) => res.width * res.height).First();
        Texture2D targetTexture = new Texture2D(cameraResolution.width, cameraResolution.height);

        UnityEngine.Windows.WebCam.PhotoCapture.CreateAsync(false, delegate (UnityEngine.Windows.WebCam.PhotoCapture captureObject)
        {
            photoCaptureObject = captureObject;

            UnityEngine.Windows.WebCam.CameraParameters c = new UnityEngine.Windows.WebCam.CameraParameters();
            c.hologramOpacity = 0.0f;
            c.cameraResolutionWidth = targetTexture.width;
            c.cameraResolutionHeight = targetTexture.height;
            c.pixelFormat = UnityEngine.Windows.WebCam.CapturePixelFormat.BGRA32;

            captureObject.StartPhotoModeAsync(c, delegate (UnityEngine.Windows.WebCam.PhotoCapture.PhotoCaptureResult result)
            {
                string filename = string.Format(@"CapturedImage{0}.jpg", tapsCount);
                string filePath = Path.Combine(Application.persistentDataPath, filename);

                // Set the image path on the FaceAnalysis class
                //FaceAnalysis.Instance.imagePath = filePath;

                photoCaptureObject.TakePhotoAsync
                (filePath, UnityEngine.Windows.WebCam.PhotoCaptureFileOutputFormat.JPG, OnCapturedPhotoToDisk);
            });
        });
    }

    /// Called right after the photo capture process has concluded
    void OnCapturedPhotoToDisk(UnityEngine.Windows.WebCam.PhotoCapture.PhotoCaptureResult result)
    {
        photoCaptureObject.StopPhotoModeAsync(OnStoppedPhotoMode);
    }

    /// Register the full execution of the Photo Capture. If successful, it will begin the Image Analysis process.
    void OnStoppedPhotoMode(UnityEngine.Windows.WebCam.PhotoCapture.PhotoCaptureResult result)
    {
        photoCaptureObject.Dispose();
        photoCaptureObject = null;

        // Request image caputer analysis
        // StartCoroutine(FaceAnalysis.Instance.DetectFacesFromImage());
    }
}
