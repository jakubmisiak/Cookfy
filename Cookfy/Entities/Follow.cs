namespace Cookfy.Entities;

public class Follow
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public virtual User User { get; set; }
    public int FollowingUserId { get; set; }
    public virtual User FollowingUser { get; set; }
}