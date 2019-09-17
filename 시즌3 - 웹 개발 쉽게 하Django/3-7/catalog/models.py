from django.db import models
import uuid
from django.urls import reverse
from django.contrib.auth.models import User
from datetime import date

# Create your models here.
class Genre(models.Model):
    name = models.CharField(
        max_length=200,
        help_text="책의 장르를 입력하세요(ex:소설)")

    def __str__(self):
        return self.name

class Author(models.Model):
    first_name = models.CharField(max_length=100)
    last_name = models.CharField(max_length=100)
    date_of_birth = models.DateField(null=True, blank=True)
    date_of_death = models.DateField("Died", null=True, blank=True)

    class Meta:
        ordering = ["last_name", "first_name"]

    def __str__(self):
        return f"{self.last_name}, {self.first_name}"

    def get_absolute_url(self):
        return reverse("author-detail", args=[str(self.id)])

class Book(models.Model):
    title = models.CharField(max_length=200)
    author = models.ForeignKey("Author", on_delete=models.SET_NULL, null=True)
    summary = models.TextField(
        max_length=1000, 
        help_text="책의 설명을 입력하세요")
    isbn = models.CharField(
        "ISBN", 
        max_length=13, 
        help_text="13자리 ISBN을 입력하세요")
    
    genre = models.ManyToManyField(Genre, help_text="책의 장르를 고르세요")

    def __str__(self):
        return self.title

    def display_genre(self):
        return ", ".join(genre.name for genre in self.genre.all())
    display_genre.short_decription = "Genre"

    def get_absolute_url(self):
        return reverse("book-detail", args=[str(self.id)])

class BookInstance(models.Model):
    id = models.UUIDField(
        primary_key=True,
        default = uuid.uuid4,
        help_text="도서의 고유번호"
    )
    book = models.ForeignKey("Book", on_delete=models.SET_NULL, null=True)
    imprint = models.CharField(max_length = 200)
    due_back = models.DateField(null=True, blank=True)

    LOAN_STATUS = (
        ("m", "작업중"),
        ("o", "대여중"),
        ("a", "대출가능"),
        ("r", "예약됨")
    )

    status = models.CharField(
        max_length = 1,
        choices = LOAN_STATUS,
        blank = True,
        default = "m",
        help_text = "책의 상태"
    )

    borrower = models.ForeignKey(User, on_delete=models.SET_NULL, null=True, blank=True)

    @property
    def is_overdue(self):
        if self.due_back and date.today() > self.due_back:
            return True
        return False

    class Meta:
        ordering = ["due_back"]
        permissions = (("can_mark_returned", "Set book as returned"),)

    def __str__(self):
        return f"{self.id}, {self.book.title}"