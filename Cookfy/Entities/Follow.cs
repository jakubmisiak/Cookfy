using System.ComponentModel.DataAnnotations;

namespace Cookfy.Entities;

public class Follow
{
    public int Id { get; set; }
    public int? FollowingUserId { get; set; }
    public int? FollowedUserId { get; set; }
}