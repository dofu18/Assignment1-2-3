﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Assignment.Data.Models;

public partial class Course
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public float Price { get; set; }

    public int Discount { get; set; }

    public string Status { get; set; }

    public string CourseDetail { get; set; }

    public string Thumbnail { get; set; }

    public string Metadata { get; set; }

    public float AvgRating { get; set; }

    public Guid Tutorid { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int SlotQuantity { get; set; }

    public virtual ICollection<BoughtCourse> BoughtCourses { get; set; } = new List<BoughtCourse>();

    public virtual ICollection<CourseCategory> CourseCategories { get; set; } = new List<CourseCategory>();

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    public virtual ICollection<OrderCourse> OrderCourses { get; set; } = new List<OrderCourse>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public virtual User Tutor { get; set; }
}