---
title: Caching Answers
layout: single
permalink: /user-guides/caching-answers/
sidebar:
  nav: "user-guides"
toc: true
---

This guide shows how to automatically cache answers to Questions.
Sometimes, an Actor needs to call a Question repeatedly to get the same answer.
For example, multiple tests in a suite might need to query the same product configurations that do not change.
Caching the answer on the first call and returning it on subsequent calls
would be much more efficient than repeatedly asking the same Question.
Boa Constrictor's support for answer caching spares programmers
from implementing their own mechanisms for storing and sharing answers.


## Writing cacheable Questions

TBD


## Creating an answer cache

TBD


## Enabling Actors to cache answers

TBD


## Caching answers when asking Questions

TBD


## Bypassing the answer cache

TBD


## Resetting the answer cache

TBD
