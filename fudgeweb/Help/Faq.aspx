<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Faq.aspx.cs"
    Inherits="Help_Faq" Title="Fudge - FAQ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .faq
        {
            display: block;
            margin-top: 10px;
            width: 350px;
        }
        .faq a
        {
            display: block;
            margin-bottom: 5px;
            margin-left: 5px;
        }
        .heading
        {
            margin-top: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="heading">
        Fudge FAQ
    </div>
    <div class="heading_description">
        New to Fudge? The faq answers general questions about the website.
    </div>
    <div class="faq">
        <a href="#q1">What is Fudge?</a> <a href="#q2">Who is running Fudge?</a> <a href="#q3">
            Who can register to Fudge?</a> <a href="#q4">Is Fudge free?</a> <a href="#q5">Why do
                I need to register to use Fudge?</a> <a href="#q6">I am concerned about privacy, what
                    can I do?</a> <a href="#q7">Where do you find your problems?</a> <a href="#q8">Can I
                        write a malicious program and hack Fudge?</a> <a href="#q9">I have a great idea / suggestion
                            / complaint about Fudge.</a> <a href="#q10">I really like Fudge, and I would like to
                                donate. How?</a> <a href="#q11">How do I submit a solution?</a>
                                <a href="#q12">Why can't I join fudge :(?</a>
    </div>
    <div class="heading">
        <a name="q1">What is Fudge?</a>
    </div>
    <p>
        Fudge is a place for students all over the world who enjoy programming to meet and
        solve problems. Fudge provides a large archive of programming problems and judges
        them in real-time. It also provides community based resources, such as a Wiki, forums,
        blogs and editorials.
    </p>
    <div class="heading">
        <a name="q2">Who is running Fudge?</a>
    </div>
    <p>
        Fudge is a project of the Florida Institute of Technology, located in Melbourne,
        Florida. It is entirely student run within the Department of Computer Sciences.
        (read more: about us)
    </p>
    <div class="heading">
        <a name="q3">Who can register to Fudge?</a>
    </div>
    <p>
        Fudge is open to students and professors worldwide. You need to provide a valid
        email address that is traceable to an educational institution. Fudge is work in
        progress: we may not have all colleges and universities, help us out by suggesting
        a school!
    </p>
    <div class="heading">
        <a name="q4">Is Fudge free?</a>
    </div>
    <p>
        Yes! Every service provided on Fudge is done so free of charge. The mission of this
        project is to encourage people interested in programming to develop one big online
        community.
    </p>
    <div class="heading">
        <a name="q5">Why do I need to register to use Fudge?</a>
    </div>
    <p>
        You register on Fudge so we can keep track of your progress, and come up with interesting
        statistics concerning your school or country. Don’t worry: we do not share your
        personal information with anybody, and never will.
    </p>
    <div class="heading">
        <a name="q6">I am concerned about privacy, what can I do?</a>
    </div>
    <p>
        You do not need to provide all your personal information to be able to register.
        We only request those pieces of information necessary to run the site. You may also
        protect your privacy by limiting what other users can find out about you. (read
        more: privacy policy)
    </p>
    <div class="heading">
        <a name="q7">Where do you find your problems?</a>
    </div>
    <p>
        Most of our problems come from ICPC sources, though other exist too. Furthermore,
        users are able to submit their own problems, and have them included in the archive,
        provided the attain to a certain level of quality and standards. We are also committed
        to protecting the rights of the authors of problems. If you think there is material
        on Fudge that violates your rights, please contact us. (read more: copyright)
    </p>
    <div class="heading">
        <a name="q8">Can I write a malicious program and hack Fudge?</a>
    </div>
    <p>
        Please don’t. Our primary focus is to make sure Fudge users are having a good time.
        We take extra effort to building a secure and standard compliant website, as well
        as running submitted code in a sandbox environment, to minimize the threat posed
        by malicious users.
    </p>
    <div class="heading">
        <a name="q9">I have a great idea / suggestion / complaint about Fudge.</a>
    </div>
    <p>
        We’d really like to hear from you. Please <a href="/Help/Contact">contact us!</a>
    </p>
    <div class="heading">
        <a name="q10">I really like Fudge, and I would like to donate. How?</a>
    </div>
    <p>
        Thanks! We’ll have a donations page setup as soon as possible.
    </p>
    <div class="heading">
        <a name="q11">How do I submit a solution?</a>
    </div>
    <p>
        Fudge allows users to submit solutions to problems in multiple languages. We use
        multiple test cases, i.e we run your program one per test case. All languages take
        input from the stdin and output to stdout. Java needs special attention as the filename
        has to be the same as the class name. <b style="color:Red">For all java submissions the class must be
            called Main.</b>
        <br />
        Try submitting one of the following solutions to
        <%=Html.LinkToProblem(52) %>:<br />
        <a href="/site/solutions/Addition.cs.txt">Addition.cs</a><br />
        <a href="/site/solutions/Addition.cpp">Addition.cpp</a><br />
        <a href="/site/solutions/Addition.java.txt">Addition.java</a><br />
        <a href="/site/solutions/Addition.py.txt">Addition.py</a><br />
        <a href="/site/solutions/Addition.hs.txt">Addition.hs</a><br />
        <a href="/site/solutions/Addition.vb.txt">Addition.vb</a>
    </p>
    <div class="heading">
        <a name="q12">Why can't I join Fudge :( ?</a>
    </div>
    <p>
        Fudge requires that users sign up with their university email. <b>Your identity is important to us!</b>
        We have this restriction so that we can keep track of user registration.
    </p>
</asp:Content>
