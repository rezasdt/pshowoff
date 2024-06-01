[System.Serializable]
public class Tag
{
    public string tag;

    public Tag(string tag)
    {
        this.tag = tag;
    }

    public static implicit operator string(Tag tag)
    {
        return tag.tag;
    }

    public static implicit operator Tag(string tag)
    {
        return new Tag(tag);
    }
}
