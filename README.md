Disclaimer :
============

THE SOFTWARE IS PROVIDED AS IS, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT
OF THIRD PARTY RIGHTS. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR HOLDERS INCLUDED IN THIS NOTICE BE LIABLE FOR ANY CLAIM, 
OR ANY SPECIAL INDIRECT OR CONSEQUENTIAL DAMAGES, OR ANY DAMAGES WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, 
WHETHER IN AN ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF OR IN CONNECTION WITH THE USE
OR PERFORMANCE OF THIS SOFTWARE.

About
======

Author
------
Rossini Tumasgiu (rossini.t at gmail dot com)

Description
-----------

Gfl is a managed wrapper written in C# 3.5 around the gflsdk C++ image library developed by Pierre Gougelet,
wich is free for non commercial use.
It use the platform invoke facility of the .Net framework to call the unmanaged methods of the library.

It provides access of various image format for reading/writing and common image transformations (rotation,crop ...).

Access to gflsdk methods comes in two manners. One can reference the Gfl binaries and use the public class GflImage
which wrap calls of unmanaged methods, or include the source code to project and use directly unmanaged methods 
through the internal class GflAPI.

For further informations concerning gflsdk, please follow : http://www.xnview.com/en/GFL/
