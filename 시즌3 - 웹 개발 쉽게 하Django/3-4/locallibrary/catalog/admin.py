from django.contrib import admin
from .models import Genre
from .models import Author
from .models import Book
from .models import BookInstance

# Register your models here.
admin.site.register(Genre)
# admin.site.register(Author)
# admin.site.register(Book)
# admin.site.register(BookInstance)

@admin.register(Author)
class AuthorAdmin(admin.ModelAdmin):
    list_display = ("last_name", "first_name", "date_of_birth", "date_of_death")

@admin.register(Book)
class BookAdmin(admin.ModelAdmin):
    list_display = ("title", "author", "display_genre")

@admin.register(BookInstance)
class BookInstanceAdmin(admin.ModelAdmin):
    list_filter = ("status", "due_back")
