namespace AppConstant.Examples;

public static class MediaType
{
    public sealed class VideoType : AppConstant<VideoType, string>
    {
        public static VideoType Mp4 => Set(".mp4");
        public static VideoType Wav => Set(".wav");
        public static VideoType Flv => Set(".flv");
    }

    public sealed class ImageType : AppConstant<ImageType, string>
    {
        public static ImageType Jpeg => Set(".jpeg");
        public static ImageType Png => Set(".png");
        public static ImageType Gif => Set(".gif");
    }
}