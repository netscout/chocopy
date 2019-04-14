from django.shortcuts import render
from blog.models import Category, Post
from django.views import generic
from django.contrib.auth.mixins import LoginRequiredMixin
from django.views.generic.edit import CreateView

# Create your views here.
def index(req):
    post_latest = Post.objects.order_by("-createDate")[:6]
    context = {
        "post_latest": post_latest
    }

    return render(req, "index.html", context=context)

class PostDetailView(generic.DetailView):
    model = Post

class PostCreate(LoginRequiredMixin, CreateView):
    model = Post
    fields = ["title", "title_image", "content", "category"]