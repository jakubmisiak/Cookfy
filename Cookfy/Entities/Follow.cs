namespace Cookfy.Entities;

public class Follow
{
    public int Id { get; set; }
    public User User { get; set; }
    public User FollowingUser { get; set; }
}