﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Assignment.Data.Models;

public partial class User
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string ProfileUrl { get; set; }

    public float Credits { get; set; }

    public string Meta { get; set; }

    public string Role { get; set; }

    public string UserName { get; set; }

    public string HashedPassword { get; set; }

    public Guid? ParentId { get; set; }

    public string Status { get; set; }

    public DateTime LastLogin { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string RefreshToken { get; set; }

    public DateTime? RefreshTokenExpires { get; set; }

    public string Token { get; set; }

    public DateTime? TokenExpires { get; set; }

    public virtual ICollection<BoughtCourse> BoughtCourseChildren { get; set; } = new List<BoughtCourse>();

    public virtual ICollection<BoughtCourse> BoughtCourseUsers { get; set; } = new List<BoughtCourse>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<User> InverseParent { get; set; } = new List<User>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual User Parent { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual ICollection<TutorProfile> TutorProfiles { get; set; } = new List<TutorProfile>();
}