using System.Diagnostics.CodeAnalysis;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading;

namespace Advent._2023.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var day1 = new Day1();

            day1.Run();
        }
    }

    internal class Day1
    {
        public void Run()
        {
            var calibrationCodes = GetCalibrationList();
            //FirstPuzzle(calibrationCodes);
            SecondPuzzle(calibrationCodes);
        }


        private void SecondPuzzle(IEnumerable<string> calibrationCodes)
        {
            var transformedCodes = calibrationCodes.Select(ReplaceWordsWithNumbers);
            FirstPuzzle(transformedCodes);
        }

        private string ReplaceWordsWithNumbers(string code)
        {
            var replacements = new Dictionary<string, int>
            {
                { "zero", 0 },
                { "one", 1 },
                { "two", 2 },
                { "three", 3 },
                { "four", 4 },
                { "five", 5 },
                { "six", 6 },
                { "seven", 7 },
                { "eight", 8 },
                { "nine", 9 },
                { "0", 0 },
                { "1", 1 },
                { "2", 2 },
                { "3", 3 },
                { "4", 4 },
                { "5", 5 },
                { "6", 6 },
                { "7", 7 },
                { "8", 8 },
                { "9", 9 }
            };

            var replaced = code;

            if (replacements.Any(x => code.Contains(x.Key)))
            {
                var dic = replacements.Where(x => code.Contains(x.Key))
                    .Select(x => new KeyValuePair<string, List<int>>(x.Key, IndexesOf(code, x.Key)))
                    .ToDictionary();

                replaced = GetSanitizedWord(dic, replacements);
            }

            System.Console.Write($"{code} - ");
            return replaced;
        }

        private string GetSanitizedWord(Dictionary<string, List<int>> dic, Dictionary<string, int> replacements)
        {
            List<int> values = new List<int>();
            foreach (var val in dic.Values)
            {
                values.AddRange(val);
            }

            values = values.Distinct().ToList();
            values.Sort();

            var myString = string.Empty;

            foreach (var index in values)
            {
                var replaceKey = dic.Where(x => x.Value.Contains(index)).Select(x => x.Key).Single(); //If this explodes, something is really wrong with this algorithm
                myString += replacements[replaceKey].ToString();
            }

            return myString;
        }

        private int IndexOf(string code, string key, int index = 0)
        {
            if (code.StartsWith(key))
                return index;
            if (code.Length == 0)
                return -1; //should not happen
            return IndexOf(new string(code.Skip(1).ToArray()), key, index + 1);
        }

        private List<int> IndexesOf(string code, string key, int index = 0)
        {
            if (code.StartsWith(key))
            {
                var returnList = new List<int> { index };
                returnList.AddRange(
                        IndexesOf(new string(code.Skip(key.Length).ToArray()),
                                  key,
                                  index + key.Length));
                return returnList;
            }

            if(code.Length == 0)
                return new List<int>();

            return IndexesOf(new string(code.Skip(1).ToArray()), key, index + 1);
        }

        private void FirstPuzzle(IEnumerable<string> calibrationCodes)
        {
            var cleaneupCalibration = calibrationCodes.Select(x => new string(x.Where(c => char.IsDigit(c)).ToArray()));

            var total = 0;

            foreach (var calibrationCode in cleaneupCalibration)
            {
                var number =
                    int.Parse($"{calibrationCode.ToCharArray().First()}{calibrationCode.ToCharArray().Last()}");
                System.Console.WriteLine($"{calibrationCode} => {number}");
                total += number;
            }

            System.Console.WriteLine("The total is " + total);

            System.Console.ReadKey();
        }



        private IEnumerable<string> GetCalibrationList(bool test = false)
        {
            if (test)
            {
                yield return "two1nine";
                yield return "eightwothree";
                yield return "abcone2threexyz";
                yield return "xtwone3four";
                yield return "4nineeightseven2";
                yield return "zoneight234";
                yield return "7pqrstsixteen";
            }
            else
            {
                yield return "fouronevhnrz44";
                yield return "eightg1";
                yield return "4ninejfpd1jmmnnzjdtk5sjfttvgtdqspvmnhfbm";
                yield return "78seven8";
                yield return "6pcrrqgbzcspbd";
                yield return "7sevenseven";
                yield return "1threeeight66";
                yield return "one1sevensskhdreight";
                yield return "rninethree6";
                yield return "eight45fourfgfive1";
                yield return "xdlnbfzxgfmhd4t";
                yield return "7tf";
                yield return "8oneldkrfcssbfeight";
                yield return "five5ninebvvfv";
                yield return "sixrhxkzcgfhltrchq3three91";
                yield return "lnxms8";
                yield return "threekv33eightninethree";
                yield return "fourxrsxhclj99twosevennine7htxdr";
                yield return "5hdhtdxgktztjdjrhkmlblsevenseven1four8";
                yield return "25xmvshkbmtkmvqpfhgq8fivefqctjm6two";
                yield return "nine533two";
                yield return "sixmbkjzpcxvfive2";
                yield return "seven3fivevhkpjvfqsfivemfdvlkhhmmvtztjf";
                yield return "3eight5threefour";
                yield return "fplrjjznseventwocrv9";
                yield return "mxqvdb5onesix84fpkzf";
                yield return "17five6mvxgkkmz2two2mf";
                yield return "lrqnqfncvvvrrpkfour92xsxfztwonehsb";
                yield return "dphngmgfhhhcjxmbmqdk3nine54";
                yield return "34xdbhnbhbmljxc55oneeight";
                yield return "cpgdcctwothreevlqmk1qpdthree";
                yield return "977ckpkmx5";
                yield return "7cx81";
                yield return "vrtgzhhrsevennzgjqhsqdxcjtm2gsj";
                yield return "dtkgj89bz5threenine";
                yield return "256mctgqnjbpr";
                yield return "smmthmrnj6threevrndhnrqq4qpnxz";
                yield return "vfrcvbgpdfjbzhchqmtjgxrmddhmggmqrgs7gxfjffivefour";
                yield return "twoeight8two3";
                yield return "threedpfour5eightthreebc3";
                yield return "csdfivefhgkjfcsvsvqsrbtplhjnine7pqhpvhjqone";
                yield return "seven3375sevenqgjndftrsh9";
                yield return "4six1npbnvfdl27hqvdrxztq";
                yield return "twopmmblrnhmq6zp1";
                yield return "34jvrtkfdcmnmblg269six5";
                yield return "fouroneeight1lpvssjskkjvhpmcb";
                yield return "kjb6";
                yield return "8rsix4";
                yield return "hcprhbhzgjklpql92pntdmxskg";
                yield return "3766zthm7lts";
                yield return "eight9sixqnkqbfrbrstbxxsixeight";
                yield return "sjncbhbcrmnmsmf4sixkhscclmrjfjgqgrdtpjqpgdsg3";
                yield return "zrxtgzscx5lgfcsbqkjnst1";
                yield return "nineonedzhsqlscgl2xpk";
                yield return "9eightljkpkkq";
                yield return "blhstnzdfttwocfjhgsix41hrx6";
                yield return "5dzsix5";
                yield return "vhlkone6five79";
                yield return "nkkm65tfnxhtmzrfpfive";
                yield return "7hcnzjn4";
                yield return "ngckn5ppcsvjsbstwo";
                yield return "5qld";
                yield return "zbgghnineninezkphpf6";
                yield return "8nine4sevenjlhlzlbztxbcrpc";
                yield return "3bcdmqxgb";
                yield return "8xvhfr3foureightwocj";
                yield return "nine68zvlfs";
                yield return "8one8xldlrninenine81seven";
                yield return "7dtvdvgnnqt6";
                yield return "htxkfzhxhfmc7631lklzppbtrbfive";
                yield return "seven89lrxddqtsvvt18";
                yield return "dnvcsqcmp7fourkrjhndsghjr";
                yield return "ztkqqqdctdeightonefivezdctxbfg7two8";
                yield return "lptlbtmmkdthreetfcttkknf53gtmvkcgmj9zvsvmsbstznmd";
                yield return "hbxr1ninefvjkkxkhbrtwo6";
                yield return "dbmxvpsvp7jdnvsdnlv";
                yield return "seventhree1";
                yield return "seven1oneseven";
                yield return "seventhreejcdvcgfthzlvl8eighttwo1";
                yield return "2fqkkgsvpjv9ninesix";
                yield return "xfmkdtxk2two";
                yield return "6fourfive";
                yield return "hpghf2cfgrmb34";
                yield return "714";
                yield return "nineonemrzxsmtwo28";
                yield return "llgnrpcdxt4eightninedl9";
                yield return "pdvhcvpmceightpfjpgvbfnrhh9";
                yield return "gvzzrvhmj464";
                yield return "592eightmvkdnjqthreedtcldng9";
                yield return "77nineonethreerqnrgczsn4hhdnpbtkxthree";
                yield return "four78zrqfourtwo";
                yield return "5chnktntwoninezqzfhthreegpmkvrsbfs";
                yield return "mpgvbnzldvbhchthreeseven4cvone";
                yield return "nmfbdpeightfour9fiveqnnmbfsheightsix";
                yield return "onexrjdhtmsdkssrgghlfsx5fourtwofive";
                yield return "5seven3zfdnqxqqdgqcphhjctjhddfggrfstqrb7";
                yield return "hcpcvfllseven8";
                yield return "qhpmcthree1sixdjkg";
                yield return "32mgqbmsn6five4c1";
                yield return "seven78";
                yield return "four8three";
                yield return "9seven5five6";
                yield return "five67";
                yield return "twopqsjvpfxdone81gsztrlxrhx5";
                yield return "9bctqg";
                yield return "jqkhxlnvfhninepzmvfmm5";
                yield return "fglgdrnlnqthreeeightfoursevenseven368";
                yield return "nineeightjvzdqnpnzf86qpffrfsp6";
                yield return "34kckxkrq";
                yield return "sixppr854";
                yield return "oneeight6";
                yield return "7onerhqgbzheightpvxjnsfdnmfive";
                yield return "68seven3";
                yield return "56ninedgzqmlvjp22";
                yield return "six69nine7threethree";
                yield return "6oneightskl";
                yield return "5six9four5twoone";
                yield return "fivefourfourhvgfqrqst99";
                yield return "2qvvnrbvjhs8dstfpfnineldkpnkskz7";
                yield return "41jrhj9nfeightonecqrdg";
                yield return "7onefour8";
                yield return "rxtdz5gbxbvdxbbpghfvljdbknqsqgtmmgrhf";
                yield return "79kftqmdnbj";
                yield return "1cf4";
                yield return "threekdrpvtsdtrdfzxtvqh3";
                yield return "zxcnsfkvfivezhcknlhcqfour9159";
                yield return "twodjdbsfive7two";
                yield return "47bnvbkseven4one7";
                yield return "nleightwo7";
                yield return "twofive5gptl3nine6twopngsq";
                yield return "eightqn32seveneightqfrfmsfgqhfourvrgdkfnxn";
                yield return "sixfivefivepnxb1jqzx4lk5";
                yield return "three97onebssix2";
                yield return "fivebvkhcmt3one6twonegf";
                yield return "xzrv997pfhxsblfive8";
                yield return "5nptkzbsixxgpmrkxponegnnqfqtgvl4";
                yield return "7mvxkfkjkxninesevenxjtrjxbj";
                yield return "jfloneightfivetwo7flmf";
                yield return "qpncksix1fourthreesixtwo7fcjjdx";
                yield return "17nineninesixhpbh";
                yield return "four88ngtbtfcqfr6tjqbqhrktf";
                yield return "vgtvfsftvljjkxnsevenltszqrgm6cgxjlqsgcqonelcdjv";
                yield return "6tznfmdzxkt";
                yield return "nineoneninefive2oneightdp";
                yield return "kmjjzvblthreethreefour1dhrsk2pfjx";
                yield return "fourfive6six2";
                yield return "rmqzgfivenineeightnine6qqnxclq";
                yield return "fourninefive3kgeightonerfxsr";
                yield return "four7qjpkrfxsvt31seventhree";
                yield return "vqgqgk69twoseven";
                yield return "qfxvmhk9";
                yield return "six1b69";
                yield return "vcbmbfjrdpnfour9";
                yield return "twoclpjtndvxj8fpxblseven";
                yield return "28xrggnqqttk544";
                yield return "39hgczgvjhvs31fivesix";
                yield return "qstz85sevennine";
                yield return "ddc5lqftnmhldv4dxfvhrxcnltr61";
                yield return "ninedhhmpzntqlninef7";
                yield return "63fourhznh";
                yield return "three39foureight";
                yield return "4zlfzjfrqsixjzb";
                yield return "2sfiveeight";
                yield return "fourfivefive46";
                yield return "fglfbbseven7445three5mfgrmkfgdc";
                yield return "8sg";
                yield return "128three3seven";
                yield return "five4threehrggrjgjpninetwo5eight";
                yield return "3hjnmvhtfxpzmdt7224txvnpnjsjf6";
                yield return "beightwobhz86fdhsthreeqjxccxhjvk45r";
                yield return "lgd2sixf7";
                yield return "6ninefour";
                yield return "hmhfeightsevenvlgdrm3five6kkjblfqbjsnxtcxrpg";
                yield return "jxvh5jjxvfdeightwon";
                yield return "1qfqdqxvtsztkkjxqz";
                yield return "1onenineppgphdtt";
                yield return "fourrppqprfmlpxbvdhpltfive1qfzr";
                yield return "xhqkzhchcmfnrpstgntwobrntnm4";
                yield return "nplmsixmx5six";
                yield return "dtvrlxzdb4clddlfivefour4one";
                yield return "67cnqshcbgsix54two8";
                yield return "7s";
                yield return "fourfourhbv28six91";
                yield return "seven6twospmcsixcvmzfj9nine1";
                yield return "1five1bphjprtm2twoxfslkqh9four";
                yield return "eight7bfzdbfccfl7";
                yield return "8eightl";
                yield return "klfpjslgdmgbxlftszdltn1";
                yield return "eightseven5threeseven";
                yield return "onenjvvqsfhgfhmtv6foureight51";
                yield return "qttmv8zxdh25hshpn4";
                yield return "7875";
                yield return "3ljptgbzsix1sgqrqlgml6sevenfktjtgtv";
                yield return "tdtwonethreeeightfivethreeqmdmc9";
                yield return "rkx1";
                yield return "eightseven26ftngc";
                yield return "tjxxsdsnxg696xhthree";
                yield return "2gdbxmxqjxnninefivenzhpmx3zqh";
                yield return "91kq5sixspxqqvpjxrp";
                yield return "mkztv5knjkzrgcz";
                yield return "3ninefpdbptdnine3qpzc";
                yield return "fourtwodg86";
                yield return "nineeight1sixfourseven";
                yield return "dvlgone1foursndcghxgxzrtsztceight8";
                yield return "6bskrcjthree";
                yield return "fourninebgcqkdlrx79";
                yield return "4sevenxfoneh";
                yield return "gmvptkcgx69vqxmckppmlnptsrndfrhtc";
                yield return "9959ngrjdgltllpmrtbrgrdpnjlgl6three";
                yield return "2c";
                yield return "3qncfdmhdcmleight1";
                yield return "3eightvfstts";
                yield return "sd7175nlpq9xhfour";
                yield return "foursevenfour477four";
                yield return "snsixthree7bmlrvdtmx";
                yield return "nhsninesevenjjhgthzhfour66seven";
                yield return "52onepcltqtlnk";
                yield return "fivedvndklt1twoxplzqtgf";
                yield return "1twohpd8xxtwojmcblxxtdk";
                yield return "nnctpnrhjtqvcpnbncpfsixseven9rrbxblhcsr";
                yield return "95ninemvhbqhpkqksixsevenone";
                yield return "5seventwothree";
                yield return "threeonetwonine2";
                yield return "cqbrtdnjzgx38twoneshm";
                yield return "4fivenine1";
                yield return "czsfivenkkqbqbbpc1";
                yield return "sjmeighttwo32one771";
                yield return "5mtmkhkhd7nineeightrq4six";
                yield return "one7vknzhj9ninesix";
                yield return "jcsqnxhp1pmfpvkdmcgzm";
                yield return "zjm5two2";
                yield return "4onejhdtnhqtlcggbxpmhtfiveone";
                yield return "nr6hvcspxjgvmlbdtgs326two";
                yield return "fivevsrfcv5fnkpftvrbnine";
                yield return "kbtwonesixbbrtjvcbblzh4";
                yield return "2fourthree";
                yield return "eightntgeightfivesix8";
                yield return "twoninefive42";
                yield return "bcjxfqlqvfslqgq52";
                yield return "tmrthree8twoxthreers";
                yield return "9fivenineeight9xdnnqkfbnkg";
                yield return "onesclfxsljp2";
                yield return "5one34";
                yield return "18nlbghthpmhntqpxnfivebfstnkcrpvxmrlrhcq";
                yield return "fourthreesix43three2fhn";
                yield return "nine5zbqpcxffpmmzxp";
                yield return "dbjgphmmvf7eight";
                yield return "6tks";
                yield return "ldnkcj6572two5sevenf";
                yield return "6jlbdqjrx86pxxnhneight";
                yield return "191pbjnlqh7vkgvrhtrkxqfgxczq";
                yield return "qpxpcnmmmone3zkm2nine5gttbdpggf";
                yield return "vnbfthmm1hckvdcqqv1sixxpldbmnxmmgrbl";
                yield return "eight5ftlsixlmrf";
                yield return "6gtqkvvmvvq5seven6mkxvxpfthreenine";
                yield return "jbskqm5gfntjjqtz";
                yield return "2eight72";
                yield return "nineplkthreesxpeightxzkfvg4";
                yield return "xzcqqnq5seven2vtsldzpfnine";
                yield return "six9t1rmbnzrkdh1";
                yield return "75kcdztv7";
                yield return "two4gbffour";
                yield return "seven8rlqhshzsixmtmpvfzjpeight5sixvh";
                yield return "7nrrcqpdqhfourthree";
                yield return "21fivenine2kjxmlmnhz6";
                yield return "3stsfthreejgzxhbseven7threebfhlml";
                yield return "dtdeightwo4jxrfivebpzf";
                yield return "qhrr2five5oneightr";
                yield return "pp2pbjgmgffctbtseven";
                yield return "737kbsxrtpcgslrmcnd4";
                yield return "qpxrkdnineqzkkhzjfqqv7twocghmkrh47";
                yield return "gfptsfour4ldrjlvzghq";
                yield return "9ninezvxqzhlgzckb8fztrsevenvlqgdm";
                yield return "7562sjpbvpeighttwo";
                yield return "qsmnktmlpznxz25gpgjqsm";
                yield return "7two5tctnfxgqvd2four";
                yield return "rls41threezlpkvtbhvk4sixnine";
                yield return "six8dpd";
                yield return "msljvkd6fourbtzfjphskfxqpjkzmzgkxvqn6hddzxj";
                yield return "mfspzjjcfour49csfjgjkdmktns";
                yield return "b3tllpmmqkktclkntlbgkkg9five";
                yield return "rqnbftghx35";
                yield return "rcgrb26five";
                yield return "sevenvkhzbljfour9czkpdltxlbqbdgcpz";
                yield return "two7fivehrqeight";
                yield return "mgmvtjl1zffzbt3two";
                yield return "trxmmtdrvh1scjhcsevenonejmzhsbttgfseven";
                yield return "one8sixtwo58";
                yield return "nrs24";
                yield return "threerrrrtjhngptrpsrq8sevensixtwoonecntmmmxtjf";
                yield return "mzgmrh5qcml";
                yield return "4lfxxdgnnjzeightninesone";
                yield return "phvmnnvzseveneighttwosixplbrb5";
                yield return "sixnineqqgbmlxdrvsrfhkrff5";
                yield return "6sdfldthgvctwosevenvvnbgqxjrn";
                yield return "7112xslv";
                yield return "sevenfoureightfivefivelkhnxb98";
                yield return "nine6pljkszdp6";
                yield return "two4xlhfhrz5kbpstsfive";
                yield return "bbgdsix5";
                yield return "sevenlcbsdlhftwozsnx1two5";
                yield return "one2jjmlqsnxx1b";
                yield return "sevensixsix5sevenzqhjzlkjgnvsxfkfvxzhq";
                yield return "lone19four1";
                yield return "1fivesixpv";
                yield return "eightfour1";
                yield return "xsbgprjfthreefoursevenrqzbcbfnbhtwo2two";
                yield return "ldtwoseven47dcrppbfbv";
                yield return "1fivexgtwosixone18three";
                yield return "nine93oneseven";
                yield return "5qdneight61xpg";
                yield return "rdzckkb5five7ggmbkzjshx";
                yield return "37lxlgmveight8";
                yield return "scqvdhm9b";
                yield return "seven9six";
                yield return "pvthreesixfourgctbsix8eightseven";
                yield return "83oneonej";
                yield return "5four83cfmnthreeone";
                yield return "fivefiveeight1";
                yield return "9tnbqpthbn5kvgmsmfmfsevenqdrksixmbmgm";
                yield return "onecrs76";
                yield return "vgdqvmt23onesix";
                yield return "dlvmvtj6eight2dxjrhdlfghfhxcsxz6tbkz";
                yield return "414sixdbzhmxthree";
                yield return "sevenfivesevencffvbdhtk5qmplrjzxnjnbnt6";
                yield return "qgqjxchmxfourknine447three";
                yield return "4tfour";
                yield return "fivercfvsvlfglbxrtvxznhr8five";
                yield return "sevensixonesix7399";
                yield return "two17";
                yield return "zcmdlqzhps5twotwojkltrxb7three";
                yield return "xllqtjpxsixsevenfivefour29";
                yield return "ninetpzpr51";
                yield return "6751three6";
                yield return "rcfpnskfivethreelbtgpltxsevenfourbtcs8eightwokb";
                yield return "eight7rfr2";
                yield return "six5onenine29";
                yield return "zjbj238";
                yield return "rgprs5fiveldvmgqktnn";
                yield return "meightwossfvkncf5sevenfivesqpgrtnnj";
                yield return "onez4eightfour62tmrjmprthree";
                yield return "foursevenjtwoc3threesixfj";
                yield return "qmmzmzlxmthreezvzxv6zgcztv";
                yield return "73mbm";
                yield return "nine251k1";
                yield return "sixhtfhzcbtq7sevenonesnkpfvsnnnine";
                yield return "seven44vp4jxbrvkzcmlnjfiveoneightvzx";
                yield return "3twoseven5";
                yield return "one8mfpgflgqkb2six7dtgftrplb";
                yield return "lrgnb4ninetwo5vjsmmb9";
                yield return "6sixq2three";
                yield return "999eightvkpcpv4";
                yield return "947fourtxdmhv";
                yield return "9msczjqjggsk3six4";
                yield return "bone1";
                yield return "5fivevvlljvmsdgqjnrhfourkstccckr";
                yield return "7hpblpnmxqlninethreesixpvpnine";
                yield return "nineeightdsrqfdnghgkeightone8mpkvfdhz";
                yield return "4sevencrjtpdq";
                yield return "mbvgsfoneltqkfnbpftxhtv7two";
                yield return "onegbvcctwo8onesevenone2six";
                yield return "vt36onetlmtqbv";
                yield return "nvvxx51";
                yield return "threefnvcvbnzvs4";
                yield return "7rxcjjdeight81587";
                yield return "mdmjsg3seventhreesn";
                yield return "dtxsdmsvjcjx819jhhpzzf9";
                yield return "9jcxsninetsl2";
                yield return "6vcllfssthkxgdgh1ghjscsrvnhrpmjllh";
                yield return "6kkhpcjhmseighteightppvdkffdcpchttzone";
                yield return "878";
                yield return "sixdkkjchkjqtllfpn31rpgxspktkgxjgcf";
                yield return "2eightsixzs3kx9";
                yield return "sixthreetwo9";
                yield return "35vtgrpcqcnvfive";
                yield return "4ninegqqlc49fourthree4two";
                yield return "czd4eight";
                yield return "tfcmfdxhhqlroneninethree43";
                yield return "8tbx";
                yield return "1xqpdxznzhsixseveneight2";
                yield return "6hg9clj";
                yield return "vfnthreevzt88";
                yield return "seven4317pkxd13";
                yield return "stvltfvs7eightcmj";
                yield return "nbrxfmsshronesevenrrnp8seven4";
                yield return "7jvtgqvm36fivegccbpkp";
                yield return "6threeeightfive8tnlninekb";
                yield return "eight1sixtp";
                yield return "17ninezjhchkvq";
                yield return "tfxvhhkltlnffournlmlsqfclqlrsk31";
                yield return "two889threezthree1two";
                yield return "five9one48twothree";
                yield return "eightseven8dqjsixlrsrscgzjpfour4";
                yield return "988nine4fk3";
                yield return "76dfkqfbqzffplx";
                yield return "9zmgrr9";
                yield return "jhsbj4twohxrgdr";
                yield return "kgcfgninetwoctqzfbhhvffive2fzlk";
                yield return "8six25";
                yield return "six35oneseven7";
                yield return "2cvxrnine33four27zk";
                yield return "4xctr9threemrckmkmjgtpxzfqdzg";
                yield return "fivelrs3ncmgdxgqhnfqrlzlnbvrqp";
                yield return "3zgblb";
                yield return "rztshfoursixfour226four";
                yield return "2hfth";
                yield return "5d94fiveninezfvprvgtnxfive";
                yield return "21four5zcb6szsjgsseight";
                yield return "2fivefpdkzxxgchfourp25lsnqjrmxmq";
                yield return "gxlbzlfzsixtwo8snhpdtgdgthkrvzsxkd3";
                yield return "five4n2";
                yield return "gscjzkgkeightqlljzx12fiveonebqzmlh";
                yield return "xhsrzj1";
                yield return "klxkdzxvvrljbnrhfive7four7";
                yield return "3sxmbgvxkmkpfqxccvj8eight2hl";
                yield return "2bcdbsllgcmftxftmzrhnfive9dgkqpqkloneskztwonesgh";
                yield return "eightsix7nine7eight69";
                yield return "five6lsgslhtzzv";
                yield return "fivesix47";
                yield return "3kxfbcjz6vmhzvkgpjbfqq34seven";
                yield return "threevlmlfm58threebkfdzd5fourkpgddjlqsz";
                yield return "sevenzdeightdtxrxzckfktrfpm7";
                yield return "qjkrfltlsdrrcm6";
                yield return "795six3hmlkdnine3";
                yield return "foursixxjnqsljktnnbtwoeight2mskdbkbtk4";
                yield return "fivetwothreeeight9sixqhqzm";
                yield return "fiveqjdksix14jlzcvck";
                yield return "16sevensixvzjnh";
                yield return "nkpshpkptggnsxp272three";
                yield return "zrv86";
                yield return "35lgqsdfive6";
                yield return "x3npthreethreercmnlphkchmrmcg9seven";
                yield return "5sixninefiveninenkxrvmk";
                yield return "jfjqrvc3";
                yield return "bbqxjhds4";
                yield return "hdkqkq5twodbbrpnbmmj";
                yield return "4twosixcfdcf515two7";
                yield return "mgzljdxqkpzbhfh62threenine74mcvrf";
                yield return "4ninesevenvnblskhgxmhvkhpmxg";
                yield return "fourvktpjlpr5sevencfvthree";
                yield return "9six9vtrkdfjkbb4five7slq1";
                yield return "dhroneight9xthreenjqk";
                yield return "eighttwo1twopxzxghjs9";
                yield return "six35ctcjrmfclone";
                yield return "d1sixmmctvr8";
                yield return "qsgcbzdtworzgzbqtrd7pttltxjsgptwomfmrzns";
                yield return "sevenseven6twocqhvmvgxn9vmlxpfourjvqsnmlbd";
                yield return "7twopc2four";
                yield return "three1nineeightgrnzbpqsvbkpbqzvmqsnm";
                yield return "91fdf985tdjvtwo9";
                yield return "svlfivetwomjhzfourone7";
                yield return "bqpssscxckkljxxtwo8dpcbpqgq";
                yield return "gjhdqvgcfive3five";
                yield return "bpqdnpbf97tgpkkmdg";
                yield return "1lksrsrlqkmzmbfgpcqq";
                yield return "kfftvhrlqpstwoone6fiverklbhqqgdpvmnine";
                yield return "xbtfhfivenine9lpklrnrrbnthreensvgrvdzpfpvzznq1";
                yield return "xkfjtzdqmhnine96sdgfveightttbhfkthree";
                yield return "fourbz2six5";
                yield return "rggoneighttwo19vfdjvjbr86zkqmfclp";
                yield return "3fourx4klhkhglvjszj8";
                yield return "glg2fournine5sixxbjseven";
                yield return "3zdpsone";
                yield return "htxgljmx262five5";
                yield return "3ftshpgpnnhgspltjrvb3cgnghgzpvzlnxvzcjone";
                yield return "threebbnrxczqninetrone7eightseven8";
                yield return "g2seven";
                yield return "threeddtgc8";
                yield return "3fivepcfgpvjzsj471";
                yield return "28zfour7seven";
                yield return "clxslzrrkbbmxbkzfnreightone6twodlztb8g";
                yield return "9eight1";
                yield return "2gvvdgnvnptwohhztfggrndzhplqgx";
                yield return "qspcbvlv48";
                yield return "1eight1sevenrlhdnhs";
                yield return "fivetpnnlnp317seven";
                yield return "n4onetwoninexnvstftnchzfxzrqts";
                yield return "2lthreeseven";
                yield return "two3twoseveneightffournbrhmseven";
                yield return "seven6fsxnfqmlg75bznfkjbch81eight";
                yield return "4dgszfourggjmjjjrr5xmddgsgp";
                yield return "b5283sbqdn36qcrxnphkg";
                yield return "9lmklvnvczeight";
                yield return "cgm2vsrmjtzmbh";
                yield return "7lhqhfm5sevenlg";
                yield return "69cmcnbqseven6bnztjrpfvx";
                yield return "eight84nine8";
                yield return "92xcmffvvbr";
                yield return "fivefivetwotwoglzqftlfpnine2";
                yield return "gtffdsrzmmnine6fourtwo";
                yield return "cjnxbkvxq7onexjjhds";
                yield return "onenine9lgkp4bxztcseven";
                yield return "bthree4";
                yield return "bqq4dbjbkftfx2cxdrdgjsixxqlcgztnv";
                yield return "twovtxqhsgx72jtgfqzfqnine4";
                yield return "87gjbghccqrsqxbflkhk2fivelmkmjzmfzxbcmjtb";
                yield return "seven7six83vdxxrfbfkfg";
                yield return "sevendnjssc7threecrccqlsf5xtgmqk";
                yield return "sjphck271threekvldbgnvtcxlcdkf";
                yield return "sevennpfbbsb3kgpdm";
                yield return "ngmhtfnkjl5fourfivegnpb";
                yield return "xthdbtwofcgdrrjmfive1five";
                yield return "89hgld6sevensevenfour6";
                yield return "36kgtmxxx1fivenineseven";
                yield return "eightdklr6";
                yield return "23zkrplrpmlpclqjkrsrxcbjone9ffjrqvsix";
                yield return "5njvthreeftxjninefnlcq";
                yield return "1qb";
                yield return "three5258kpcrl";
                yield return "szggtxbvql443";
                yield return "3threemvbzslgqdninesevenxzgbfkcg64";
                yield return "6fivefmhtdcbpx";
                yield return "five4fivesix";
                yield return "9ninebrdczqjpnine";
                yield return "q33ninesix6threepbqjnjdb";
                yield return "onelvhdtljltmjgdsnine1eight7";
                yield return "one2seventwo";
                yield return "4jnfchm5jtqgbmnctrsix";
                yield return "5six3";
                yield return "eight9hklfhjnine2onerl6";
                yield return "2nzncz9fourdcqmmcfgbgsqchvnrdnrcthree";
                yield return "2337";
                yield return "tvqqfkxcg81eightfivedxmb";
                yield return "mxkcbqkptqbgqvgjxbrbnine3dtzhkmeightsixdxdhbpxd";
                yield return "two9szthjhjmcfseven4";
                yield return "rkcsnrl2btjgvbmlnine1jdjczcskjg622";
                yield return "six75three9";
                yield return "5threethreetwo8";
                yield return "1eightngz";
                yield return "seven3sixn2";
                yield return "three7pdfjtzfv5threesxpgrbkmx2khxcsbdblh";
                yield return "onezzrzpbvsmflzrlfmv4h46five5";
                yield return "6jdv";
                yield return "smseven3eightfivegzlkm3";
                yield return "nine7eight5";
                yield return "sixthreeone54four";
                yield return "7mrxpdc6xvkhcmtwo4";
                yield return "3crrcmxzqzfchtnvnh";
                yield return "nhxtwoseven84";
                yield return "9sixvltbsix";
                yield return "6ctsnvvgm9sixpjhmtskzggtgsevenseven";
                yield return "ninehfqcvgkgqf167one1sixeightwocf";
                yield return "dtsg38sixtwonephq";
                yield return "rpfbnzn2vhjxeightqdfbqmpgpdqn";
                yield return "cncdqcbgzt8";
                yield return "xbhqsrfp6357ftq7";
                yield return "1rnnz277";
                yield return "8threeonejvkfldcrnxjflltr9";
                yield return "l8sevenseven8qmm5plczqnkrhvkppb";
                yield return "one7qjgnslmrdnbsone451xffltxrvdk";
                yield return "hmcmtzcl59";
                yield return "8qqffcfhj59";
                yield return "rzlkxvgckrfvnone2rjtwo";
                yield return "two9ninesevennine37";
                yield return "2x5";
                yield return "4cjkghzhtfrdhhcrjvt2";
                yield return "cpbhsgbzt8nzcjqnine5cntrngcqj";
                yield return "9three4sixeightcxmvmgglf";
                yield return "31dptlfdphx7three";
                yield return "fiveonethreesixppgpbzr8jlrpzhr";
                yield return "731bdfdvtpqb3pcbrsxvtxbtgphzdbsixfive";
                yield return "1gxmppthreetwo1xrslpjqq";
                yield return "2three3four4";
                yield return "pbdlpv56eight52";
                yield return "qmspclflmrckcbbqxnthreeone7dczkjhxnvjtkx";
                yield return "78dslfsggrkp";
                yield return "vntwonesixfourvsmmmgpghm3";
                yield return "three981flhdlvkseventwo";
                yield return "fqv9twopvk";
                yield return "pthpkvdqcn1";
                yield return "qtz763six8eightzxbcp";
                yield return "67vtscljxslhfpt8";
                yield return "2mqfhbpn";
                yield return "1jtdcgrlrpdmjbmlptgdzjfgqb";
                yield return "1twoeight6znzgxf8one";
                yield return "9gngqbsprhmhpsgzps";
                yield return "gjqsfhfivenine4pfhxthtjthgsm38";
                yield return "five1twofiveninetwofive2jrtzvtf";
                yield return "ninethreetbmcb2nine";
                yield return "96fztmrseveneightbhfpqjbsixvhjfvzzjdc";
                yield return "8threenineonejhhcmnnrzsnbzcstzqtfsthree";
                yield return "9onesixdttqqjvfour8";
                yield return "sixtcfdhjlddncmskrfoursix72";
                yield return "868fivevsstbpxtwo";
                yield return "four2onesix";
                yield return "four4ktnggkpfvtwozqqpsckb7";
                yield return "eightfour4threeninesixnbtzsevenjxpsn";
                yield return "sixfcmnbdflf7four7";
                yield return "mqvjfdhhbqkpkdx5sevenhrhkphqrsix";
                yield return "cb3bngnjgmjng";
                yield return "6jfive";
                yield return "roneightseven27fivetwofourfourbzshqvxdc";
                yield return "ltwone4zpvhninenm1eightktmkbpvlkkhzhvfc";
                yield return "84dslslds";
                yield return "bphdhtwo2ckktccnrqjptwo";
                yield return "six27glzs";
                yield return "threedxsdjsgmlvrtnmzjbvt71kdfiverpddfrczjhs";
                yield return "ddqlb4mtpfthreerkztrmxvbhhnjqmmdxdmlffcrhoneightfcg";
                yield return "qr1";
                yield return "56threenine6hgvtwotwofnjbppqppb";
                yield return "vb2fourninethree4knvzpjcj";
                yield return "threetlsevenfourgcjmzprone5three";
                yield return "5fourrrqhbrbq7three41";
                yield return "five8qpxdsdpxeightphjvbmtp";
                yield return "9one522";
                yield return "f3pklscq";
                yield return "4sjfiveht3xknmkndsgfjvl1zqdt";
                yield return "three4vjjg691";
                yield return "99grcttgddb";
                yield return "7pgh8sixsm5five";
                yield return "one185kjbftmplhqdkfvrh";
                yield return "4kzrchdkv2tgbrkghhnine6";
                yield return "sccnhhmmfournvbbvrlpfsevenfivefive8";
                yield return "frrrdkzvfg7blgqj7threecqhvvgfd";
                yield return "fivehsfkmbvxddkfrrngfivefive4four6";
                yield return "9onetwozcgntxrdzeight4zpnqkxsfpcggtrjjk4";
                yield return "5jbmn4nbzbmmkbvxstchl";
                yield return "fivenine4vvtfhrhbdeight4294";
                yield return "onefour749oneghm";
                yield return "one97czzjrrddjsb1nine";
                yield return "nine4sixsix";
                yield return "4eightthree3three";
                yield return "45rxpndjsmq6fourqpmk";
                yield return "seven93";
                yield return "5173five7kvgqpttwonemh";
                yield return "ninefive2n1eight";
                yield return "452zdvjfczspmcknblsmzznvrlhkmn";
                yield return "47onexntj2sixsixthree";
                yield return "nine9threesixfsmnfour51six";
                yield return "pgtfttdkjmz47";
                yield return "5threeone9gr2vjjcrbslcp";
                yield return "jsfour7fnmqgsxlnine2gsmmkgvj5gbvx";
                yield return "bktwonngqchlqghpnmlzxnthkpjgcjsr98eight";
                yield return "tczsjfcxfgjzbxeightnine5twoone";
                yield return "48";
                yield return "99flzfrrjdp69zxjdphrbbsixthreetpzn";
                yield return "five82fourxczgknkgk";
                yield return "six7fourbeight";
                yield return "oneeight4kxsdrpsix169dh";
                yield return "mnine9";
                yield return "npxvqkbpdtddhlggthreempqqblzfmmvc1mknf";
                yield return "56v9two24zdscdf";
                yield return "tmcbxhvhptvtwosdhltwo4mvjphvjjrsglrxbjrthree";
                yield return "threefive3skzsspkfqp61";
                yield return "three5three315xgjgphdr";
                yield return "8jvqvpspkqgbl";
                yield return "1vpkglvlxs2sixgc6eight3";
                yield return "hzshxqgxzttwovkfive3fgmctvvdfsbjhdt";
                yield return "prsqrjcrrj9nxgq2";
                yield return "dfsfxtdj19lqlblbmb1";
                yield return "ltlmvf81xsztgzpljcxx8";
                yield return "lrqxphqhthreetwolqjsixtwo81";
                yield return "nstpljlpt1sixnineone91";
                yield return "one4xcrhhdhsvveightfive";
                yield return "rfmdkpmbrhrnzqtqnrvdv4two5";
                yield return "ndf8mccqlzqrjjcpzzpbjrmclpkvkkltvgfsfour";
                yield return "two8three11eightwom";
                yield return "five5xpzcmtxnqj";
                yield return "7238fouronerxjvqtmhtljcnine";
                yield return "zdfmfkfbkb2seven";
                yield return "3fbkzxmz";
                yield return "21one699eightcqfs1";
                yield return "68vpgggr";
                yield return "97s9eight7";
                yield return "pkxvtvgtrdsqkgclnkxhjr3threelr";
                yield return "954lcthpqcscz";
                yield return "twothreevshxfjvnq6";
                yield return "rsnkplhfive3pmpkknnvbf6bvxqhbjhc";
                yield return "hhxhhpnrsevensztgxnqhmzcsnsxgblc7754";
                yield return "grjdsxzhjldjckhbxbrqlbbjhntjjv79";
                yield return "five7sevenfive";
                yield return "ghljgxzbfourcvqqnnine1rrmftfr";
                yield return "koneightbfxgjjzkkgsqzhhdctg4";
                yield return "cgtxldhfourtwoseven9zlhgrchhvfhrgmqdgnine";
                yield return "51twothree6x";
                yield return "sevenrfseven1sqmhzrg";
                yield return "bpnmzncsix3one6gzpzfsrfbp36";
                yield return "stjgqone46seven";
                yield return "gzrnkk7ninefour";
                yield return "fivefgnfkn4tzjxtjlrfive1";
                yield return "bdpgnthrf83nffzpeightmj";
                yield return "twotwovzbvbrsgseventhree15";
                yield return "7ninexqjggfvqndsdcg2";
                yield return "three8fivehpfxncpxv4three";
                yield return "twofive6rctmlhrxxlthreerrrvstvvqx";
                yield return "rgfdddkmvkfvjspmzzp6hxprqlvseven";
                yield return "threexhqqhone3six67l";
                yield return "eightseven56krtbthree";
                yield return "seven99";
                yield return "6j1czlmxsmqgzvrcjjscnineseven";
                yield return "ffnrkvchddftmhklsbxfourxmqnrnlrvvsevenone93";
                yield return "jlnzkqfvnsix8four74eightone";
                yield return "56eight1sqthzbsfpsevenhdlqkkqjqnqtbseven";
                yield return "dfsxgdsfnrktlhllrxqp5onevmfklvgxqhmlhgqhd8";
                yield return "cvtdzcsfive6rhggdz1tfzkffglck593";
                yield return "ninet715two1";
                yield return "gknjznhzvjrmqtkdbb5";
                yield return "xnrvvntwo22";
                yield return "9nlpcldct8nine5eightkjzmqskpnb";
                yield return "eightsix7ninetwopjslsgvbseven1";
                yield return "p2dcg49one";
                yield return "xcdthreessvvzts67bkqchqgqcf";
                yield return "31ncbk97";
                yield return "kxftjfivegmkcfqbvsl8threeqnrnrsixbdqzl";
                yield return "91fourpqfghjncnine8bfxqxdjckfckdzgsl";
                yield return "2vhrdjpcdlg";
                yield return "mrcjfivetwo6threenine";
                yield return "2ljldxrdg94four";
                yield return "gnoneghkgtt256";
                yield return "mxplnslnrsxpzlgx3nineonefive";
                yield return "63mzsvmsix";
                yield return "6fourninejpsfnineseven";
                yield return "fourninefive3threethreecmbseven";
                yield return "761zzctnddfour2one";
                yield return "dzonesixseven1two";
                yield return "fiver31oneeighteightwov";
                yield return "3kpbcxsthreeone5";
                yield return "nineonektx48drsgpktpns255";
                yield return "qfqtwone7nine374";
                yield return "xnsevenm9";
                yield return "seven8fourdptllvrggqzcnqfourtrbslxxgrrgj";
                yield return "onesix1sixngvmpbjctrkztfour1one";
                yield return "sjmxkkvddt89jlgfvhnlhlzzhdvp";
                yield return "jbk6";
                yield return "six4hkfbxrbg1ff7six";
                yield return "3six6fourthree6two";
                yield return "5sevensixonefour26";
                yield return "pgnzkprhj1rxsqftblftjgngthqdgmbdfmcpxxtsj8seven";
                yield return "17nine2kcnqv";
                yield return "ckzfgltmnqnkgzkxdfmncp8nine8";
                yield return "ccfourgfpdfrgmvdbvdvpd3";
                yield return "9bgqggggrbggmdrjkfivesvknmpthree";
                yield return "82sqghgstwoeightt";
                yield return "nk1slsckcn87cfsbggnsfnps67";
                yield return "1onethreefxdcqfourtpzqtwosix";
                yield return "pfivek4";
                yield return "99vnxnscjpeight4dthlk5eightwovl";
                yield return "rmtwonehkt6lczt8vfxmkzkxsb2five";
                yield return "one89bchlvvhtjz3zbspjtmkqkfourpznpfeight";
                yield return "foursqmchjpccone7ninenine2";
                yield return "1dvsgvone236six";
                yield return "six63three9";
                yield return "bvvgtrmmxs5scdzvcpseven7gdnxvczneight5oneightzbr";
                yield return "cv18cvdgxmrjsgnztgjrb4threefive9";
                yield return "ninesixsevennseventhree73";
                yield return "44dxhfgjt2gntv95";
                yield return "rbbhmmqbrc9twofour";
                yield return "4bqfivefiverdp658four";
                yield return "hpsslx88four";
                yield return "one8hmjrstmmeighttwodrmpm7five9";
                yield return "4ninen";
                yield return "9seven9fd442";
                yield return "3vqdtnhqg";
                yield return "hldgmmnfmzeighteightseven48";
                yield return "8msmbtzlvsf1ph4lkqddcbhcnp";
                yield return "jnkxqmghbpjslmgsbvlhtrr1njgrx2twozfk5";
                yield return "fxchjbvgl8mdhtckn8seven";
                yield return "rllvfrffhf13sevenc98";
                yield return "vgbprsjllsrhkltdrcknmfk1rpeightjpkxvjsrm";
                yield return "six81hgfzghnn8fivenseven";
                yield return "ninesrqjnt9onetwoeight63j";
                yield return "pbrrb9fqjhhfhfh9twokhhdsghvjkvkpj";
                yield return "1nineztppgztbdxvrgqvs46";
                yield return "jbqrdsqtfninefxtg1nzmdcqvg";
                yield return "41ninefiveninefiveone96";
                yield return "mmsfsjeightthreeqgbfbnvgnv5four4threepggz";
                yield return "6threehqq5four";
                yield return "mpthqgsix86two";
                yield return "5onesevenfive61";
                yield return "8ninetwoeightj31";
                yield return "7fivekxzhlxdsevenzn";
                yield return "sevenshxtsixzdfjvpcsc5jvjhgzbssbrqtwonemx";
                yield return "phls1";
                yield return "148nxbhkjr";
                yield return "2l8threeqfpddrjxrzlqldtqjseventwo";
                yield return "k7one";
                yield return "92sixtwotwoglmnxdnxz";
                yield return "9six4xdh";
                yield return "9five9nine";
                yield return "k21xc";
                yield return "sixonegsbffgtsevenzjbrone4chq";
                yield return "eightfive67fiveseven3four9";
                yield return "8lxzjjjshhgpvkmzcjjljr1fiveltt1";
                yield return "nine24eightts5xnfgtlpng5rkq";
                yield return "lhtwone1six8156zttxfdn5";
                yield return "69sevenmbkjdjbl";
                yield return "hnm8fivetwo";
                yield return "6threezc";
                yield return "lhmtbt7ltb6";
                yield return "six1eight4crjfmmxonenine";
                yield return "fhrvhfkp4xjstfour";
                yield return "kxeightwoseventlrvhfrkhrhfive34twos";
                yield return "sfvdlttlrfourthreevzksseven736";
                yield return "7crlzrzone8";
                yield return "fjsfpfivejxvqnq16threesixqxdmjz6";
                yield return "fkrjfjkgbjnine63fxfkfvphbjngnfqbhb2seventlbbr";
                yield return "596";
                yield return "dgfrd288six";
                yield return "783nine";
                yield return "two8fivefpkjllbnqgqlkqms7mmhbsrnhsxnbmjv";
                yield return "seven79one9";
                yield return "fourmrxqtmg21lnztkhx";
                yield return "3one728sixfive";
                yield return "fourconemgdcch8three";
                yield return "bfsfktfbvqfiveonecljcbqfnine2five";
                yield return "5eight2sevenzxpsk";
                yield return "6twotrvkhqrsppcxhjvjlkhcjrqpqvqxrmxqt";
                yield return "77kxkrpzr";
                yield return "grlrh36sevenone49";
                yield return "9fiveeight8";
                yield return "five4jbspqscf";
                yield return "4zzzjxghvcj";
                yield return "6xhrgnhxzx16xrknine";
                yield return "84nqhnxcdldthreeseventwodnbpszp";
                yield return "twoseven3xzpxpgjvbgldlqsgf";
                yield return "ncsfckp5cgv4jrbkf";
                yield return "9eightsixtcdkzlbl";
                yield return "sjfxx4";
                yield return "onebshtqkhslhfvhgqtvsnhqfhone4";
                yield return "fgggsixtwo4pl5";
                yield return "nvrr2twohknrgcxtpltwosgfbnlszeight";
                yield return "five1bzfdfsrz";
                yield return "4xvtwo2";
                yield return "five2eighth4";
                yield return "2jlnlmbqccrgkmjqninethreethreenine4";
                yield return "gtxxdnxqzlfive5seven";
                yield return "fivegjkqh46eight";
                yield return "33z";
                yield return "48sixscgcghlqjheight";
                yield return "vkfzkkxxnj17fivedt";
                yield return "onejlhdglpkjsixtwo24";
                yield return "4bssmdxpone1bnxjtwo";
                yield return "pbsixsfxddk3fivefive43eight";
                yield return "fivemnmqbzonetdgvmsone48";
                yield return "dbjeightwo9nine9";
                yield return "eight123kkptmzpqjj1";
                yield return "15krrcpgqshrhxrxpgone8kbkvgjlghc";
                yield return "45eight323hvnbtbqqtwoeight";
                yield return "veightwo2dsqjg";
                yield return "three77q6";
                yield return "1hshtr1vsbhfctfpdl1threem";
                yield return "nineninethreevvgbclnkcmzhctgjtwoseven4";
                yield return "seven3foursix4four";
                yield return "fhvm96bbxrmtcgcthree4rthree7";
                yield return "65crr";
                yield return "7ljvcqtceightbnkpbxqgfvfzxmfmctdjctgcc";
                yield return "llj659nine5rl";
                yield return "two8fivep3ddmpdhngm8cf";
                yield return "hljxhkvbk4five5k";
                yield return "threeninefive9";
                yield return "jncnpkxjvst2eightrjltjc";
                yield return "4psqtnkxrc";
                yield return "eightqtscplvmkbrlnine449nbnxtkz7";
                yield return "sixnvkbxlxninevhtdvf77zrnjzxfbdlsix1";
                yield return "eight2mznnrmhnmclxdcdzjsfourtxvgmdzvk";
                yield return "fkghbffpnmqblcrfivethreethree195";
                yield return "pxgpcrmpone1one";
                yield return "5pfkktxpfjgsixp5lbhrvv373";
                yield return "six8seven2fseven6";
                yield return "eight9crdxxxgpbprtdxpfsgglkst1";
                yield return "8zbmntvnpfoureightxz7four";
                yield return "3599lx5";
                yield return "seven9six4fdqr3vgq8";
                yield return "fiveone2twosevenfive";
                yield return "twotwo35sixczdx";
                yield return "two62964mdhbqhrfkf";
                yield return "phtrfmmkzonedcsnqdvrghvvf36thqdxfrh";
                yield return "dxnqsdhfivefour2385threeone";
                yield return "7onetwo";
                yield return "pfzrvpphj7phnzqmkbbbfvstwosevensix";
                yield return "69foursixonefive2";
                yield return "jszkfs9hkcsnxtzfs3";
                yield return "gqb1gzbglssfxqjvffivexlphdd7";
                yield return "2one777";
                yield return "qcczbcm4sevenseven";
                yield return "4threevxhtx";
                yield return "tdxxf8";
                yield return "jlxc5fivesixeight";
                yield return "sbszlkpjgfivevtsjscqdpv5eight56";
                yield return "bbpxxtwo798";
                yield return "fiveztdspgfive97zfmrvnxeighttwosix";
                yield return "51threetwovrbv5";
                yield return "fourfhxzgvbgdceightqnqrvmb7";
                yield return "11ngqhghdsevensthhs9";
                yield return "sevensevenrqmpsf4hnpqxmgbhskllksnkfourfour7";
                yield return "vheightwo5twothreehbzcrs";
                yield return "2fournpvcpksffj1";
                yield return "mxmldpfsevenpfcvhff9twonineeight";
                yield return "9pcqfrtkrtwo";
                yield return "hxxzppmlfive6cktznkfgmnctjfjpxvgdfszk7ptwo";
                yield return "vjjhjlqsvknineggvjdnnpltzrrqz7";
                yield return "9xdjlnqc";
                yield return "seven1lmdffjjqgxone";
                yield return "fiveseven9";
                yield return "5931zmck";
                yield return "twofourvzrdjmhbnl9onedrtcnl";
                yield return "4trmsevenhbsone";
                yield return "87pzvbcrdvzg3vfszszfds13";
                yield return "5three38gccjdm6six";
                yield return "xcszzrlpltbxs48mcs";
                yield return "ksjzvcgptnq3bfhrfx";
                yield return "twoninethreekhvndq8jfkrfpzsr71six";
                yield return "nine8zmnsdzxqhf4nine6nine";
                yield return "bsdvvggfrjvbvqrtjzbkzmcp64fourthree";
                yield return "nceighteightggrtjsblxdhpxsix7";
                yield return "ninebv8";
                yield return "136tjpsfdgnine";
                yield return "9q";
                yield return "seven1two";
                yield return "mtmzmplcnrfive3rtmhcxcxpsixeight2eight2";
                yield return "qclnh4";
                yield return "5d";
                yield return "tmvbmvh176jngdjlhszlfcbzv";
                yield return "two77jzfncqnm";
                yield return "onexdcrstcqlsixnmtxndzksfsvrxccmjj45";
                yield return "threeone58rzpfivenine";
                yield return "118zgl48";
                yield return "tfhdfive1gzz59bkztx";
                yield return "r3three4fourgzlgljdrmnmnjntssbpvkpmfdsjlbl";
                yield return "24hz";
                yield return "four8sixone6";
                yield return "nineqfggh3svpvlfzpfxoneeight6twohzb";
                yield return "941";
                yield return "onefivejnbgncqfzcsixdqd8rxjd2";
                yield return "9fivesixfivefivesix647";
                yield return "noneight25fhqrvv";
                yield return "eightninephmksl9dvhvcbvdldthree";
                yield return "threegr8";
                yield return "6fzqndfour5nine";
                yield return "4bl2zhcnpqvxthreemkjfqmdctsqzbkllfgvsmtt";
                yield return "9three13";
                yield return "hvbftpsbprhzx5";
                yield return "one7threer57";
                yield return "psdkpvjkzrs3sixfive";
                yield return "ngsqbpfbt34";
                yield return "gbdh7threexnszmtwo";
                yield return "64cmmt9sevenh8qdgmcpplgsj";
                yield return "fivervlsbzjsfiveqbmlrvlzqn8twoeight1vxzkjf";
                yield return "bznrgbrmnzvhp1twosix";
                yield return "97gldxj";
                yield return "fsjbjcklxp9nine2";
                yield return "hpgm7three";
                yield return "ljxcrlfive9bf";
                yield return "four894zmzmllzgkv";
                yield return "fivetmbkmmrjg9seven1tsix1";
                yield return "88sixgprtzhfzsncm4";
                yield return "dbdqkgtdxdjxhk3lsqvsmmtonexstlrplstvqvvmthree";
                yield return "sixfbvmqxbhbgfbl3";
                yield return "jkdshphdg1";
                yield return "sevenrkrvtwofivenine4fourcmjmmdvzvh";
                yield return "553";
                yield return "cqthreeone2ppfflh3fiveseven";
                yield return "5fivekxfzpzjsd42sevenzgfourtwo";
                yield return "55fivekrtckjphnlplbcbxbzf";
                yield return "1pgsfgdf755";
                yield return "5dncccmkpqtwocmmlltvbg";
                yield return "two23eight9lnjk";
                yield return "1148pdtcl1eight5oneights";
                yield return "4ltrvvtxfhcp8eight4dpfhmqeight";
                yield return "44xtvnlrcpb";
                yield return "9knzcfpkv2hqntgqkfgtsix1eight8";
                yield return "rlgmxxmpsk9";
                yield return "1one9";
                yield return "1six9fsvbrrgxqpsevensbnzshmb9";
                yield return "ftgbfqrzslqrcmmeightnjjrrkvhntcv1djmbqztrkvlqfkshoneightggd";
                yield return "one5twofqqgcdvzkllqgxhjpmkhsjpthree";
                yield return "sevenxtmq34two9gnvrvxfjmgq";
                yield return "nineeight6mkvbfour6four";
                yield return "6dcrpx8sixseven2bbszpncx3";
                yield return "6keighteightfive6six13";
                yield return "sixfive69sevenschkmdrvs";
                yield return "dzdgsmgcmkthxddd1921two7";
                yield return "fivenine9eighttworlrccrjzseven3";
                yield return "3onefoursix";
                yield return "6jzqksxpk";
                yield return "qfjhv1";
                yield return "sevennssgfpzt2bcxldkphfour1sixnine5";
                yield return "sx1zhzzpzonevrllpblsfnxzknmpconefktdt";
                yield return "mdvqxbgjhzprrhvqhdt6kfq";
                yield return "sclszppxlpzvzpscvqc791twochlgnsf4";
                yield return "krtjqmseven6tbllzgjcghsix1";
                yield return "114";
                yield return "75kp";
                yield return "rbrftcblxcknine4eight";
                yield return "ffmsgbqf33jcjktprgmczzkd6";
                yield return "v9zjhcvjjkr716";
                yield return "three49oneightf";
                yield return "ninesix2twobvdrbsvjrmvhsdhncsqhcfour";
                yield return "5thmkvcrfsix87hjhgbrxxfgseven";
                yield return "five8fourone24fqjknjq";
                yield return "xfmeight8";
                yield return "7onenine";
                yield return "fivesixfive2six9hn";
                yield return "7six441";
                yield return "1gjkphqtwo";
                yield return "fourhzgxqtxggfpprrmtfqsdhc2fdxnjdgx64five";
                yield return "threeninejdzzrbpmfhjcqdsix8two2bb";
                yield return "7877pzrbtcsddmrffzdsmqlqkjsix";
                yield return "5four3eight";
                yield return "15nine1";
            }
        }
    }
}